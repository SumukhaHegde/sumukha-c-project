using MediatR;

namespace Application.Activity.Commands
{
    public record CancelActivityCommand(int ActivityId) : IRequest<string>
    {
    }
}
