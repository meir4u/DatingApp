using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Reflection;

namespace DatingApp.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg=> {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                });
            return services;
        }
    }
}
