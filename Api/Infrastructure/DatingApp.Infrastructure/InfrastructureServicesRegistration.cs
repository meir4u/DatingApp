using DatingApp.Domain.Interfaces;
using DatingApp.Infrastructure.Data;
using DatingApp.Infrastructure.Data.Repository;
using DatingApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DatingApp.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                //opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            



            return services;
        }
    }
}
