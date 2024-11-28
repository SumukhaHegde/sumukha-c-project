using Application.Activity.Interfaces;
using Core.Entities;
using Dapper;
using Npgsql;

namespace Infrastructure.Persistance.Repositories.Activity
{
    public class ActivityWriteRepository : IActivityWriteRepository
    {
        private readonly IDBConnectionFactory _dbConnectionFactory;
        private readonly string connectionString;

        public ActivityWriteRepository(IDBConnectionFactory dBConnectionFactory)
        {
            _dbConnectionFactory = dBConnectionFactory;
            connectionString = _dbConnectionFactory.CreateDBConnection(Enum.ConnectionType.Write);
        }

        public async Task<int> CreateActivityAsync(ActivityEntity activity)
        {
            const string insertQuery = @"
            INSERT INTO activity (name, description, date, category, city, venue, iscancelled, hostid)
            VALUES (@Name, @Description, @Date, @Category, @City, @Venue, @IsCancelled, @HostId)
            RETURNING id;";

            using (var connection = new NpgsqlConnection(connectionString))
                return await connection.ExecuteScalarAsync<int>(insertQuery, activity);
        }

        public async Task DeleteActivity(int Id)
        {
            var deleteQuery = "DELETE FROM Activity WHERE Id = @Id";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var activity = await connection.ExecuteAsync(deleteQuery, new
                {
                    Id
                });
                connection.Close();
            }
        }

        public async Task UpdateActivity(ActivityEntity activity)
        {
            const string updateQuery = @"
        UPDATE activity
        SET 
            name = COALESCE(@Name, name),
            description = COALESCE(@Description, description),
            date = COALESCE(@Date, date),
            category = COALESCE(@Category, category),
            city = COALESCE(@City, city),
            venue = COALESCE(@Venue, venue),
            iscancelled = COALESCE(@IsCancelled, iscancelled)
        WHERE id = @Id;";

            using var connection = new NpgsqlConnection(connectionString);
            await connection.ExecuteAsync(updateQuery, new
            {
                activity.Name,
                activity.Description,
                activity.Date,
                activity.Category,
                activity.City,
                activity.Venue,
                activity.IsCancelled,
                activity.Id
            });
        }

        public async Task AddAttendeeAsync(UserActivityEntity attendee)
        {
            const string insertAttendeeQuery = @"
    INSERT INTO user_activity (user_id, activity_id, firstname, bio,username)
    VALUES (@UserId, @ActivityId, @firstname, @Bio,@UserName);";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.ExecuteAsync(insertAttendeeQuery, attendee);
                connection.Close();
            }
        }
    }
}
