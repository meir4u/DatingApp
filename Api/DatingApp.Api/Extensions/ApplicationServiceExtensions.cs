﻿using DatingApp.Api.Filters;
using DatingApp.Api.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DatingApp.Application;
using DatingApp.Infrastructure;
using DatingApp.Domain.Services;
using DatingApp.Infrastructure.Data;
using DatingApp.Infrastructure.Params;    

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

            services.AddScoped<LogUserActivityFilter>();

            services.AddSignalR();
            services.AddSingleton<PresenceTracker>();
            //services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
