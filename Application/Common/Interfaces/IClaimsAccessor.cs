using Application.Authentication.DTO;
using System.Security.Claims;

namespace Application.Common.Interfaces
{
    public interface IClaimsAccessor
    {
        ClaimsPrincipal Claims { get; }
        ClaimsData ClaimsData { get; }
        string TenantToken { get; }
    }
}
