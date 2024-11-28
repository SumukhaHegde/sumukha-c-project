using Application.Activity.DTO;
using MediatR;

namespace Application.Activity.Commands
{
    public record CreateActivityCommand(CreateActivityRequest createActivityRequest) : IRequest
    {
    }
}
