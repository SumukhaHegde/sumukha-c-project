using Application.Activity.Interfaces;
using Application.Authentication.Common;
using Application.Authentication.Interface;
using Application.Migrations.Common;
using Application.Photos.Interface;
using Infrastructure.Authentication;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Repositories;
using Infrastructure.Persistance.Repositories.Activity;
using Infrastructure.Persistance.Repositories.ActivityRepository;
using Infrastructure.Persistance.Repositories.PhotosRepository;
using Infrastructure.Persistance.Repositories.UserRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static async Task<IServiceCollection> RegisterInfrastructureDependenciesAsync(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.AddScoped<IDBConnectionFactory, DBConnectionFactory>();
            services.AddScoped<IMigrationWriteRepository, MigrationWriteRepository>();
            services.AddScoped<IActivityReadRepository, ActivityReadRepository>();
            services.AddScoped<IActivityWriteRepository, ActivityWriteRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserReadOnlyRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<ICurrentUserContext, CurrentUserContext>();
            services.AddScoped<IActivityAttendeeReadRepository, ActivityAttendeeReadRepository>();
            services.Configure<CloudinarySetting>(configurationManager.GetSection("Cloudinary"));
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();



            services.RegisterAuthenticationDependencies(configurationManager);


            return services;
        }

        private static IServiceCollection RegisterAuthenticationDependencies(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.Configure<JwtSetting>(configurationManager.GetSection("JWTSetting"));
            var jwtSettings = new JwtSetting();
            configurationManager.GetSection("JWTSetting").Bind(jwtSettings);

            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience
                };
            });
            return services;
        }
    }
}
