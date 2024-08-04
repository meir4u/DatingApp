using DatingApp.Api.Helpers;
using DatingApp.Api.Services;
using DatingApp.Api.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DatingApp.Application;
using DatingApp.Infrastructure;
using DatingApp.Domain.Services;
using DatingApp.Infrastructure.Data;

namespace DatingApp.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //cors
            services.AddCors();
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            //application layers
            services.ConfigureInfrastructureServices(configuration);
            services.ConfigureApplicationServices();
            

            //token service added
            services.AddScoped<ITokenService, TokenService>();

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddScoped<LogUserActivity>();

            services.AddSignalR();
            services.AddSingleton<PresenceTracker>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
