using MediatR;

namespace Application.Activity.Commands
{
    public class DeleteActivityCommand : IRequest
    {
        public int Id { get; set; }
    }
}
