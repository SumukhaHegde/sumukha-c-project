using Application.Activity.DTO;
using MediatR;

namespace Application.Activity.Queries
{
    public class GetActivityByIdQuery : IRequest<ActivityDto>
    {
        public int Id { get; set; }
    }
}
