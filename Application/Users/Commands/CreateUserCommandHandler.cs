using Application.Authentication.Interface;
using Core.Entities;
using Mapster;
using MediatR;

namespace Application.Users.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserWriteRepository _userWriteRepository;

        public CreateUserCommandHandler(IUserWriteRepository userWriteRepository)
        {
            _userWriteRepository = userWriteRepository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.userRequest.Adapt<UserEntity>();
            await _userWriteRepository.createUser(user);
        }
    }
}
