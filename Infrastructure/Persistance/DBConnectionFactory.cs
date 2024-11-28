using Infrastructure.Persistance.Enum;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Persistance
{
    public class DBConnectionFactory : IDBConnectionFactory
    {
        protected IDbConnection? connection { get; set; }
        public readonly String _connectionString;
        public DBConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public string CreateDBConnection(ConnectionType connectionType)
        {
            if (connectionType == null)
            {
                throw new Exception("Connection Type is invalid");
            }
            return _connectionString;
        }
    }
}
