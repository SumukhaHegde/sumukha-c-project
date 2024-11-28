using Application.Authentication.Interface;
using Application.Common.Interfaces;
using Application.Users.DTO;
using Mapster;

namespace Infrastructure.Authentication
{
    public class CurrentUserContext : ICurrentUserContext
    {
        private readonly IClaimsAccessor _claimsAccessor;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public CurrentUserContext(IClaimsAccessor claimsAccessor, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _claimsAccessor = claimsAccessor;
            _userReadOnlyRepository = userReadOnlyRepository;

        }

        public async Task<UserDetailsLite> GetCurrentUser()
        {
            if (_claimsAccessor.ClaimsData != null)
            {
                string loggedInUserName = _claimsAccessor.ClaimsData.Email;
                var currentUser = await _userReadOnlyRepository.GetUserDetailsFromUserName(loggedInUserName);

                return currentUser.Adapt<UserDetailsLite>();
            }
            return null;
        }
    }
}
