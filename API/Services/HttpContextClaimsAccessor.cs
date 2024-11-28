using Application.Authentication.DTO;
using Application.Common.Interfaces;
using System.Security.Claims;

namespace API.Services
{
    public class HttpContextClaimsAccessor : IClaimsAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;



        public HttpContextClaimsAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal Claims
        {
            get
            {
                ClaimsPrincipal claimsPrincipal = _contextAccessor?.HttpContext.User;
                if (claimsPrincipal?.Claims == null) { return null; }
                return claimsPrincipal;
            }
        }

        public ClaimsData ClaimsData
        {
            get
            {
                if (Claims != null)
                {
                    return new ClaimsData
                    {
                        NameIdentifier = Claims.FindFirstValue(ClaimTypes.Name),
                        Email = Claims.FindFirstValue(ClaimTypes.Email),
                        UniqueName = Claims.FindFirstValue(ClaimTypes.GivenName)
                    };
                }
                return null;
            }
        }

        public string TenantToken => throw new NotImplementedException();

    }
}
