using Core.Entities;
using MediatR;

namespace Application.Authentication.Query
{
    public class GetLoggedInUserProfileQuery : IRequest<LoggedInUserProfile>
    {
    }
}
