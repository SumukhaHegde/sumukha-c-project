using Application.Authentication.Interface;
using Core.Entities;
using Dapper;
using Npgsql;

namespace Infrastructure.Persistance.Repositories.UserRepository
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly IDBConnectionFactory _dbConnectionFactory;
        private readonly string connectionString;

        public UserWriteRepository(IDBConnectionFactory dBConnectionFactory)
        {
            _dbConnectionFactory = dBConnectionFactory;
            connectionString = _dbConnectionFactory.CreateDBConnection(Enum.ConnectionType.ReadOnly);
        }
        public async Task createUser(UserEntity user)
        {
            var query = "INSERT INTO Users(email,firstname, lastname,password,image) VALUES (@Email,@FirstName, @LastName, @Password,null)";
            var pass = PasswordHasher.HashPassword(user.Password);
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var users = await connection.ExecuteAsync(query, new
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = pass,
                });
                connection.Close();
            }
        }
    }
}
