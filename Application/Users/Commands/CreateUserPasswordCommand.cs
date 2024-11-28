using MediatR;

namespace Application.Users.Commands
{
    public class CreateUserPasswordCommand : IRequest
    {
        public int UserId { get; set; }
        public string PasswordHash { get; set; }
    }
}
