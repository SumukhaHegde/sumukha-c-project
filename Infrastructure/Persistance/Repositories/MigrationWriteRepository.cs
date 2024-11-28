using Application.Migrations.Common;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistance.Repositories
{
    public class MigrationWriteRepository : IMigrationWriteRepository
    {
        public Task<int> DeketeNugratuibAsync(long version)
        {
            throw new NotImplementedException();
        }

        public Task<string> MigrateDown(long version)
        {
            throw new NotImplementedException();
        }

        public async Task<string> MigrateUp(long? version)
        {
            using (var serviceProvider = CreateServices())
            using (var scope = serviceProvider.CreateScope())
            {
                var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                if (runner.HasMigrationsToApplyUp(version))
                {
                    runner.MigrateUp();
                }
                var result = "Migrations has been applied succefully to the database";
                return await Task.FromResult(result);
            }

        }

        private ServiceProvider CreateServices()
        {
            var migrationAssembly = typeof(MigrationWriteRepository).Assembly;
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddPostgres11_0()
                .WithGlobalConnectionString("Server=localhost;Port=5432;Username=postgres;Password=Sumukha;Database=Reactivities")
                .ScanIn(migrationAssembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}
