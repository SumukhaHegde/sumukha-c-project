using Application.Activity.Interfaces;
using Application.Authentication.Interface;
using Application.Users.DTO;
using MediatR;

namespace Application.Activity.Commands
{
    public class CancelActivityCommandHandler : IRequestHandler<CancelActivityCommand, string>
    {
        private readonly IActivityReadRepository _activityRepository;
        private readonly IActivityWriteRepository _activityWriteRepository;
        private readonly ICurrentUserContext _currentUserContext;
        private readonly IUserReadOnlyRepository _userReadRepository;

        public CancelActivityCommandHandler(
            IActivityReadRepository activityRepository,
            IActivityWriteRepository activityWriteRepository,
            ICurrentUserContext currentUserContext,
            IUserReadOnlyRepository userReadOnlyRepository)
        {
            _activityRepository = activityRepository;
            _activityWriteRepository = activityWriteRepository;
            _currentUserContext = currentUserContext;
            _userReadRepository = userReadOnlyRepository;
        }
        public async Task<string> Handle(CancelActivityCommand request, CancellationToken cancellationToken)
        {

            // Get the current user
            UserDetailsLite currentUser = await _currentUserContext.GetCurrentUser();

            // Retrieve the activity to cancel
            var activity = await _activityRepository.GetActivityById(request.ActivityId);

            if (activity == null)
            {
                return null;
            }

            // Ensure the user is the host
            if (activity.HostId != currentUser.Id)
            {
                return "Only the host can cancel this activity.";
            }

            // Set the activity as canceled
            activity.IsCancelled = true;
            await _activityWriteRepository.UpdateActivity(activity);

            return "Activity cancelled";
        }
    }
}
