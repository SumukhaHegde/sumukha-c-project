using Application.Migrations.Common;
using FluentResults;
using MediatR;

namespace Application.Migrations.Command
{
    public class MigrationUpCommandHandler : IRequestHandler<MigrationUpCommand, Result<string>>
    {
        private IMigrationWriteRepository _migrationWriteRepository;

        public MigrationUpCommandHandler(IMigrationWriteRepository migrationWriteRepository) {

            _migrationWriteRepository = migrationWriteRepository;
        }
        public async Task<Result<string>> Handle(MigrationUpCommand request, CancellationToken cancellationToken)
        {
            var result = await _migrationWriteRepository.MigrateUp(request.Version);
            return await Task.FromResult(result);
        }
    }
}
