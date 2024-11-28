using Application.Authentication.Interface;
using Core.Entities;
using Mapster;
using MediatR;

namespace Application.Authentication.Query
{
    public class GetLoggedInUserProfileQueryHandler : IRequestHandler<GetLoggedInUserProfileQuery, LoggedInUserProfile>
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ICurrentUserContext _currentUserContext;
        public GetLoggedInUserProfileQueryHandler(IUserReadOnlyRepository userReadOnlyRepository, ICurrentUserContext currentUserContext)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _currentUserContext = currentUserContext;

        }

        public async Task<LoggedInUserProfile> Handle(GetLoggedInUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userDetails = await _currentUserContext.GetCurrentUser();
            var response = await _userReadOnlyRepository.GetUserDetailsFromUserId(userDetails.Id);
            return response.Adapt<LoggedInUserProfile>();
        }
    }
}
