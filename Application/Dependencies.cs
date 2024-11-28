using Application.Activity.Commands;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterApplicationDependencies(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(ctg =>
            {
                ctg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CreateActivityCommandValidator>();
            services.AddValidatorsFromAssembly(typeof(Dependencies).Assembly);


            return services;
        }
    }
}
