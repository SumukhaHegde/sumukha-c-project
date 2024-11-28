using Application.Activity.DTO;
using MediatR;

namespace Application.Activity.Queries
{
    public class GetActivitiesQuery : IRequest<List<ActivityDto>>
    {
    }
}
