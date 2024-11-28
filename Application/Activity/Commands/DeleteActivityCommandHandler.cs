using Application.Activity.Interfaces;
using MediatR;

namespace Application.Activity.Commands
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
    {
        private readonly IActivityWriteRepository _activityWriteRepository;

        public DeleteActivityCommandHandler(IActivityWriteRepository activityWriteRepository)
        {
            _activityWriteRepository = activityWriteRepository;
        }
        public Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            return _activityWriteRepository.DeleteActivity(request.Id);
        }
    }
}
