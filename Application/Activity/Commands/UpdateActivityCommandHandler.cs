

using Application.Activity.Interfaces;
using Application.Authentication.Interface;
using Application.Users.DTO;
using MediatR;
namespace Application.Activity.Commands
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, string>
    {
        private readonly IActivityReadRepository _activityRepository;
        private readonly IActivityWriteRepository _activityWriteRepository;
        private readonly ICurrentUserContext _currentUserContext;

        public UpdateActivityCommandHandler(
            IActivityReadRepository activityRepository,
            IActivityWriteRepository activityWriteRepository,
            ICurrentUserContext currentUserContext)
        {
            _activityRepository = activityRepository;
            _activityWriteRepository = activityWriteRepository;
            _currentUserContext = currentUserContext;
        }
        public async Task<string> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {// Get current user details
            UserDetailsLite currentUser = await _currentUserContext.GetCurrentUser();

            // Retrieve the activity
            var activity = await _activityRepository.GetActivityById(request.UpdateRequest.ActivityId);


            if (activity == null)
                return null;

            // Check if the user is authorized to update (e.g., only the host can update)
            if (activity.HostId != currentUser.Id)
                return "Only the host can update this activity.";

            // Update only the provided fields
            if (request.UpdateRequest.Name != null) activity.Name = request.UpdateRequest.Name;
            if (request.UpdateRequest.Description != null) activity.Description = request.UpdateRequest.Description;
            if (request.UpdateRequest.Date.HasValue) activity.Date = request.UpdateRequest.Date.Value;
            if (request.UpdateRequest.Category != null) activity.Category = request.UpdateRequest.Category;
            if (request.UpdateRequest.City != null) activity.City = request.UpdateRequest.City;
            if (request.UpdateRequest.Venue != null) activity.Venue = request.UpdateRequest.Venue;
            if (request.UpdateRequest.IsCancelled.HasValue) activity.IsCancelled = request.UpdateRequest.IsCancelled.Value;

            // Persist the updated activity
            await _activityWriteRepository.UpdateActivity(activity);
            return "Activity updated successfully";
        }
    }
}
