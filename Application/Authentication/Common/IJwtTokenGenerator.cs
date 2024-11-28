namespace Application.Authentication.Common
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int userId, string firstName, string lastName, string userName);
    }
}
