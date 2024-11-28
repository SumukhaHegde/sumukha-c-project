using MediatR;
using FluentResults;

namespace Application.Migrations.Command
{
    public record MigrationUpCommand : IRequest<Result<string>>
    {
        public long? Version { get; set; }
    }
    public record MigrationDownCommand : IRequest<Result<string>>
    {
        public long Version { get; set; }
    }
}
