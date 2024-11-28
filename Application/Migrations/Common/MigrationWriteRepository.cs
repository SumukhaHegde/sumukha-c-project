
namespace Application.Migrations.Common
{
    public interface IMigrationWriteRepository 
    {
        Task<string> MigrateUp(long? version);
        Task<string> MigrateDown(long version);
        Task<int> DeketeNugratuibAsync(long version);

    }
}
