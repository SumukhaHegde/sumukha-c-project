using Application.Authentication.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSetting _jwtSetting;

        public JwtTokenGenerator(IOptions<JwtSetting> jwtSetting)
        {
            if (jwtSetting != null)
                _jwtSetting = jwtSetting.Value;
            else throw new ArgumentNullException(nameof(jwtSetting));
        }
        public string GenerateToken(int userId, string firstName, string lastName, string userName)
        {
            try
            {

                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret)), SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.GivenName,firstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName,lastName),
                    new Claim(JwtRegisteredClaimNames.Email,userName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Name,userId.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: _jwtSetting.Issuer,
                    audience: _jwtSetting.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(Double.Parse(_jwtSetting.ExpireInMinutes)),
                    signingCredentials: signingCredentials
                    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch
            {

                throw;
            }
        }
    }
}
