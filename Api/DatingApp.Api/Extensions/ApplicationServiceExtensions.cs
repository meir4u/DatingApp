using DatingApp.Api.Data;
using DatingApp.Api.Data.Repository;
using DatingApp.Api.Helpers;
using DatingApp.Api.Interfaces;
using DatingApp.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DatingApp.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            //cors
            services.AddCors();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //token service added
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddScoped<LogUserActivity>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            return services;
        }
    }
}
