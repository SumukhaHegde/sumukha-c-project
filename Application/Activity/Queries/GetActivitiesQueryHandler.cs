using Application.Activity.DTO;
using Application.Activity.Interfaces;
using Mapster;
using MediatR;

namespace Application.Activity.Queries
{
    public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, List<ActivityDto>>
    {
        private readonly IActivityReadRepository _activityReadRepository;

        public GetActivitiesQueryHandler(IActivityReadRepository activityReadRepository)
        {
            _activityReadRepository = activityReadRepository;
        }
        public async Task<List<ActivityDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            var activities = await _activityReadRepository.GetAllActivitiesAsync();
            var activityDtos = activities.Adapt<List<ActivityDto>>(); // Ensure mapping configuration is correct

            return activityDtos;
        }
    }
}
