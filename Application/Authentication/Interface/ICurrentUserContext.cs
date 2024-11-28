using Application.Users.DTO;

namespace Application.Authentication.Interface
{
    public interface ICurrentUserContext
    {
        Task<UserDetailsLite> GetCurrentUser();
    }
}
