using Application.Authentication.Common;
using Application.Authentication.Interface;
using Core.Entities;
using FluentResults;
using MediatR;

namespace Application.Authentication.Query
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
    {
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;


        public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserReadOnlyRepository userRepository)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            UserEntity userDetails = await _userRepository.GetUserDetailsFromUserName(request.request.Email);
            if (userDetails != null)
            {
                bool valid = await _userRepository.ValidateUser(request.request.Email, request.request.Password);
                if (!valid)
                {
                    return Result.Fail<AuthenticationResult>("Invalid Credentials");

                }
                else
                {
                    return new AuthenticationResult()
                    {
                        Email = userDetails.Email,
                        UserId = userDetails.Id,
                        FirstName = userDetails.FirstName,
                        LastName = userDetails.LastName,
                        Token = _jwtTokenGenerator.GenerateToken(userDetails.Id, userDetails.FirstName, userDetails.LastName, userDetails.Email)
                    };
                }
            }
            else
            {
                return Result.Fail<AuthenticationResult>("User not found");
            }
        }

    }

}
