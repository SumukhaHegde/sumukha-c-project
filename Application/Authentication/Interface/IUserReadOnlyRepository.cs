using Core.Entities;

namespace Application.Authentication.Interface
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> ValidateUser(string email, string password);
        Task<UserEntity> GetUserDetailsFromUserName(string userName);
        Task<UserEntity> GetUserDetailsFromFirstName(string firstName);
        Task<UserEntity> GetUserDetailsFromUserId(int userId);
    }
}
