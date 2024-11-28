using Application.Activity.DTO;
using Application.Activity.Interfaces;
using Application.Authentication.Interface;
using Mapster;
using MediatR;

namespace Application.Activity.Queries
{
    public class GetActivityByIdHandler : IRequestHandler<GetActivityByIdQuery, ActivityDto>
    {
        private readonly IActivityReadRepository _activityReadRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public GetActivityByIdHandler(IActivityReadRepository activityReadRepository, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _activityReadRepository = activityReadRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task<ActivityDto> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        {
            // Retrieve the activity details, including host and attendees, from the read repository
            var activity = await _activityReadRepository.GetActivityById(request.Id);
            var users = await _userReadOnlyRepository.GetUserDetailsFromUserId(activity.HostId);

            if (activity == null) return null; // Return null if activity is not found

            // Map Activity entity to ActivityDto
            var activityDto = activity.Adapt<ActivityDto>();

            // If your DTO needs a specific structure for attendees, adjust here
            activityDto.Attendees = activity.Attendees.Select(a => new AttendeeDto
            {
                UserId = a.UserId, // Assuming UserId is the identifier for the user
                FirstName = a.FirstName, // Ensure Username is being mapped
            }).ToList();

            return activityDto; // Return the mapped DTO
        }
    }
}
