using Application.Activity.Interfaces;
using Core.Entities;
using Dapper;
using Npgsql;

namespace Infrastructure.Persistance.Repositories.Activity
{
    public class ActivityReadRepository : IActivityReadRepository
    {
        private readonly IDBConnectionFactory _dbConnectionFactory;
        private readonly string connectionString;
        private readonly IActivityAttendeeReadRepository _activityAttendeeReadRepository;

        public ActivityReadRepository(IDBConnectionFactory dBConnectionFactory, IActivityAttendeeReadRepository activityAttendeeReadRepository)
        {
            _dbConnectionFactory = dBConnectionFactory;
            connectionString = _dbConnectionFactory.CreateDBConnection(Enum.ConnectionType.ReadOnly);
            _activityAttendeeReadRepository = activityAttendeeReadRepository;
        }

        public async Task<ActivityEntity> GetActivityById(int id)
        {

            const string query = @"
    SELECT 
    a.id, a.name, a.description, a.date, a.category, a.city, a.venue, a.iscancelled, a.hostid AS HostId,
    u.id AS UserId, u.email AS Username, u.firstname AS FirstName, u.lastname AS LastName
FROM 
    activity a 
LEFT JOIN 
    users u ON u.id = a.hostid
    WHERE a.id = @Id";
            List<UserActivityEntity> OtherAttendees = await _activityAttendeeReadRepository.GetAllAttendeesForActivity(id);

            using (var connection = new NpgsqlConnection(connectionString))
            {

                var activityDictionary = new Dictionary<int, ActivityEntity>();


                var activities = await connection.QueryAsync<ActivityEntity, UserActivityEntity, ActivityEntity>(
                    query,
                     (activity, attendee) =>
                    {
                        // Check if the activity already exists in the dictionary
                        if (!activityDictionary.TryGetValue(activity.Id, out var currentActivity))
                        {
                            currentActivity = activity;
                            currentActivity.HostName = attendee.FirstName + " " + attendee.LastName;
                            currentActivity.Attendees = new List<UserActivityEntity>();
                            activityDictionary.Add(currentActivity.Id, currentActivity);
                        }

                        if (attendee != null)
                        {
                            // Ensure the properties are set correctly
                            attendee.ActivityId = currentActivity.Id;
                            attendee.FirstName = attendee.FirstName; // Assuming this is your UserActivityEntity property
                                                                     // Add this property to your UserActivityEntity if needed
                                                                     // Add this property to your UserActivityEntity if needed
                            currentActivity.Attendees.Add(attendee);
                        }

                        foreach (var OtherAttendee in OtherAttendees)
                        {

                            // Check if the fetched attendees list is not null or empty
                            if (OtherAttendee != null && OtherAttendee.UserId != activity.HostId)
                            {
                                currentActivity.Attendees.Add(OtherAttendee);
                            }
                        }
                        return currentActivity;
                    },
                    new { Id = id },
                    splitOn: "UserId"
                );


                // Return the first activity found, or null if not found
                return activityDictionary.Values.FirstOrDefault();
            }
        }

        public async Task<List<ActivityEntity>> GetAllActivitiesAsync()
        {
            const string query = @"
SELECT 
    a.id, a.name, a.description, a.date, a.category, a.city, a.venue, a.iscancelled, a.hostid AS HostId,
    u.id AS UserId, u.email AS Username, u.firstname AS FirstName, u.lastname AS LastName
FROM 
    activity a 
LEFT JOIN 
    users u ON u.id = a.hostid;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                var activityDictionary = new Dictionary<int, ActivityEntity>();

                // First, get the basic activity and host data
                var activities = await connection.QueryAsync<ActivityEntity, UserActivityEntity, ActivityEntity>(
                    query,
                    (activity, attendee) =>
                    {
                        if (!activityDictionary.TryGetValue(activity.Id, out var currentActivity))
                        {
                            currentActivity = activity;
                            currentActivity.HostName = attendee.FirstName + " " + attendee.LastName;
                            currentActivity.Attendees = new List<UserActivityEntity>();
                            activityDictionary.Add(currentActivity.Id, currentActivity);
                        }

                        // Add attendee if valid
                        if (attendee != null && attendee.UserId != 0)
                        {
                            currentActivity.Attendees.Add(attendee);
                        }

                        return currentActivity;
                    },
                    splitOn: "UserId"
                );

                // Fetch other attendees for each activity outside of the QueryAsync call
                foreach (var currentActivity in activityDictionary.Values)
                {
                    var otherAttendees = await _activityAttendeeReadRepository.GetAllAttendeesForActivity(currentActivity.Id);
                    foreach (var otherAttendee in otherAttendees)
                    {
                        if (otherAttendee != null && otherAttendee.UserId != currentActivity.HostId)
                        {
                            currentActivity.Attendees.Add(otherAttendee);
                        }
                    }
                }

                return activityDictionary.Values.ToList();
            }
        }


    }
}
