using Core.Entities;

namespace Application.Authentication.Interface
{
    public interface IUserWriteRepository
    {
        Task createUser(UserEntity user);
    }
}
