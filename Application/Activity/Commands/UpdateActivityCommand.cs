using Application.Activity.DTO;
using MediatR;

namespace Application.Activity.Commands
{
    public class UpdateActivityCommand : IRequest<string>
    {
        public UpdateActivityRequest UpdateRequest { get; }

        public UpdateActivityCommand(UpdateActivityRequest updateRequest)
        {
            UpdateRequest = updateRequest;
        }

    }
}
