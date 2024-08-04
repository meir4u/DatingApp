
using DatingApp.Api.Extensions;
using DatingApp.Api.Middleware;
using DatingApp.Api.SignalR;
using DatingApp.Domain.Entities;
using DatingApp.Infrastructure.Data;
using DatingApp.Infrastructure.Logging;
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

            // Add Serilog to the logging pipeline
            builder.Host.UseSerilog();

            // Add services to the container.

            builder.Services.AddControllers();

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
                .WithOrigins("http://localhost:4200", "https://localhost:4200"));
            
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
