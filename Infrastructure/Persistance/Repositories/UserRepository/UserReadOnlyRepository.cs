using Application.Authentication.Interface;
using Core.Entities;
using Dapper;
using Npgsql;

namespace Infrastructure.Persistance.Repositories.UserRepository
{
    public class UserReadOnlyRepository : IUserReadOnlyRepository
    {

        private readonly IDBConnectionFactory _dbConnectionFactory;
        private readonly string connectionString;

        public UserReadOnlyRepository(IDBConnectionFactory dBConnectionFactory)
        {
            _dbConnectionFactory = dBConnectionFactory;
            connectionString = _dbConnectionFactory.CreateDBConnection(Enum.ConnectionType.ReadOnly);
        }
        public async Task<UserEntity> GetUserDetailsFromUserName(string email)
        {

            var query = "select * from Users where email = @email";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var users = await connection.QuerySingleOrDefaultAsync<UserEntity>(query, new { email = email });
                connection.Close();
                return users;
            }

        }

        public async Task<UserEntity> GetUserDetailsFromFirstName(string firstName)
        {

            var query = "select * from Users where firstname = @firstName";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var users = await connection.QuerySingleOrDefaultAsync<UserEntity>(query, new { userName = firstName });
                connection.Close();
                return users;
            }

        }

        public async Task<UserEntity> GetUserDetailsFromUserId(int UserId)
        {

            var query = "select * from Users where id = @UserId";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var users = await connection.QuerySingleOrDefaultAsync<UserEntity>(query, new { UserId = UserId });
                connection.Close();
                return users;
            }

        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            var UserQuery = "select * from Users where email = @email";
            var pass = PasswordHasher.HashPassword(password);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var UsersDetails = await connection.QuerySingleOrDefaultAsync<UserEntity>(UserQuery, new { email = email });

                var Valid = PasswordHasher.VerifyPassword(password, UsersDetails.Password);

                connection.Close();
                return Valid;
            }
        }


    }
}
