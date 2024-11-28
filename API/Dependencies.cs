using API.Services;
using Application.Common.Interfaces;

namespace API
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterAPIDependencies(this IServiceCollection services, IConfigurationManager configurationManager)
        {

            services.AddHttpContextAccessor();
            services.AddScoped<IClaimsAccessor, HttpContextClaimsAccessor>();
            return services;
        }
    }
}
