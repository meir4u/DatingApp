using DatingApp.Domain.Adapter.Google;
using DatingApp.Domain.Interfaces;
using DatingApp.Domain.Services;
using DatingApp.Infrastructure.Adapters.Google;
using DatingApp.Infrastructure.Data;
using DatingApp.Infrastructure.Data.Repository;
using DatingApp.Infrastructure.Email;
using DatingApp.Infrastructure.Params;
using DatingApp.Infrastructure.Scheduling.Email;
using DatingApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Spi;
using System;
using System.Net.Mail;
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
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.Configure<GoogleSettings>(configuration.GetSection("Google"));

            // Setup FluentEmail with Razor as the templating engine
            services
                .AddFluentEmail("your-email@example.com", "Your Name")
                .AddRazorRenderer() // Add Razor as the template renderer
                .AddSmtpSender(new SmtpClient("smtp.example.com") // Configure SMTP client
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-email-password"),
                    EnableSsl = true
                });


            //adapters added
            services.AddHttpClient<IGoogleAuthAdapter, GoogleAuthAdapter>();

            //Repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILikesRepository, LikesRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //token service added
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IGoogleTokenValidatorService, GoogleTokenValidatorService>();

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddScoped<IAuthenticationUserService, AuthenticationUserService>();

            //email with it jobs
            services.AddTransient<IEmailService, EmailService>();
            services.AddSingleton<IEnhancedEmailService, EnhancedEmailService>();
            services.AddSingleton<IJobFactory, EmailJobFactory>();



            return services;
        }
    }
}
