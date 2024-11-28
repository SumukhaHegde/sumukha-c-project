using Infrastructure.Persistance.Enum;

namespace Infrastructure.Persistance
{
    public interface IDBConnectionFactory
    {
        string CreateDBConnection(ConnectionType connectionType);
    }
}
