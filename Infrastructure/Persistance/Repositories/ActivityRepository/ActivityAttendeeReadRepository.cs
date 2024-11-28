using Application.Activity.Interfaces;
using Core.Entities;
using Dapper;
using Npgsql;

namespace Infrastructure.Persistance.Repositories.ActivityRepository
{
    public class ActivityAttendeeReadRepository : IActivityAttendeeReadRepository
    {

        private readonly IDBConnectionFactory _dbConnectionFactory;
        private readonly string connectionString;

        public ActivityAttendeeReadRepository(IDBConnectionFactory dBConnectionFactory)
        {
            _dbConnectionFactory = dBConnectionFactory;
            connectionString = _dbConnectionFactory.CreateDBConnection(Enum.ConnectionType.ReadOnly);
        }

        public async Task<List<UserActivityEntity>> GetAllAttendeesForActivity(int ActivityId)
        {
            const string query = @"select ua.user_id as UserId, activity_id as activityId, firstname as firstname, bio as bio, username as username from user_activity ua 
where activity_id = @ActivityId";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                var attendees = await connection.QueryAsync<UserActivityEntity>(query, new { ActivityId = ActivityId });
                return attendees.ToList();
            }

        }
    }
}
