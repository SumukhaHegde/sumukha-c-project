using Application.Activity.Interfaces;
using Application.Authentication.Interface;
using Application.Users.DTO;
using Core.Entities;
using MediatR;

namespace Application.Activity.Commands
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand>
    {
        private readonly IActivityWriteRepository _activityWriteRepository;
        private readonly ICurrentUserContext _currentUserContext;

        public CreateActivityCommandHandler(
            IActivityWriteRepository activityWriteRepository,
            ICurrentUserContext currentUserContext)
        {
            _activityWriteRepository = activityWriteRepository;
            _currentUserContext = currentUserContext;
        }

        public async Task Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            UserDetailsLite userDetails = await _currentUserContext.GetCurrentUser();

            // Map command to entity
            var activity = new ActivityEntity
            {
                Name = request.createActivityRequest.Name,
                Description = request.createActivityRequest.Description,
                Date = request.createActivityRequest.Date,
                Category = request.createActivityRequest.Category,
                City = request.createActivityRequest.City,
                Venue = request.createActivityRequest.Venue,
                HostId = userDetails.Id,
                IsCancelled = false // Default value for new activities
            };

            var activityId = await _activityWriteRepository.CreateActivityAsync(activity);

            // Add attendee after the activity is created
            var attendee = new UserActivityEntity
            {
                ActivityId = activityId, // Ensure you have this property to link to the activity
                UserId = userDetails.Id,
                FirstName = userDetails.FirstName,
                Bio = userDetails.FirstName,
            };

            // Now add the attendee to the database
            await _activityWriteRepository.AddAttendeeAsync(attendee);
        }
    }
}
