
using DatingApp.Api.Extensions;
using DatingApp.Api.Filters;
using DatingApp.Api.Middleware;
using DatingApp.Api.SignalR;
using DatingApp.Domain.Entities;
using DatingApp.Infrastructure.Data;
using DatingApp.Infrastructure.Logging;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace DatingApp.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            SerilogSetup.ConfigureLogger();

            var builder = WebApplication.CreateBuilder(args);

            // Configure Kestrel to listen on specific ports
            //builder.WebHost.ConfigureKestrel(options =>
            //{
            //    options.ListenAnyIP(8080); // HTTP
            //    options.ListenAnyIP(443, listenOptions => listenOptions.UseHttps()); // HTTPS
            //});

            // Log the current directory
            var currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current Directory: {currentDirectory}");

            // Add Serilog to the logging pipeline
            builder.Host.UseSerilog();
            builder.Services.AddSingleton(Log.Logger);
            builder.Services.AddLogging(loggingBuilder =>
                loggingBuilder.AddSerilog(dispose: true));

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ActionMethodExceptionFilter>();
                options.Filters.Add<ActionMethodLoggingFilter>();
            });

            builder.Services.AddApplicationServices(builder.Configuration);

            builder.Services.AddIdentityServices(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder=>builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("https://dattingapp-datingapp.piemei.easypanel.host", "http://dattingapp-datingapp.piemei.easypanel.host", "http://localhost:4200", "https://localhost:4200", "https://localhost:44387", "http://localhost:44387", "http://localhost:50578", "https://localhost:50578"));

            app.Urls.Add("https://dattingapp-datingapp.piemei.easypanel.host");
            app.Urls.Add("http://dattingapp-datingapp.piemei.easypanel.host");

            if (builder.Environment.IsDevelopment())
            {
                // Add COOP middleware
                app.Use(async (context, next) =>
                {
                    context.Response.Headers.Append("Cross-Origin-Opener-Policy", "unsafe-none");
                    await next();
                });
            }
            else
            {
                // Add COOP middleware
                app.Use(async (context, next) =>
                {
                    context.Response.Headers.Append("Cross-Origin-Opener-Policy", "same-origin");
                    await next();
                });
            }
            

            app.UseAuthentication();
            //tell what you allowed to do.
            app.UseAuthorization();

            //for angular files
            app.UseDefaultFiles(); //looks for index.html
            app.UseStaticFiles(); //look for wwwroot folder

            app.MapControllers();
            app.MapHub<PresenceHub>("hubs/presence"); //signalR
            app.MapHub<MessageHub>("hubs/message"); //signalR
            app.MapFallbackToController("Index", "Fallback");

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var dataContext = services.GetRequiredService<DataContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

                await dataContext.Database.MigrateAsync();

                await Seed.ClearConnections(dataContext);
                await Seed.Seedusers(userManager, roleManager);
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "An Error occurred during migration");
            }
            app.Run();
        }
    }
}
