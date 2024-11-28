using Application.Users.DTO;
using MediatR;

namespace Application.Users.Commands
{

    public record CreateUserCommand(CreateUserRequest userRequest) : IRequest
    {
    }

}
