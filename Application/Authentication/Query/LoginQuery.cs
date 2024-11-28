using Application.Authentication.Common;
using Application.Authentication.DTO;
using FluentResults;
using MediatR;

namespace Application.Authentication.Query
{
    public record LoginQuery(LoginRequest request) : IRequest<Result<AuthenticationResult>>
    {

    }
}
