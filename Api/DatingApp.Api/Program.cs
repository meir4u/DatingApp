
using DatingApp.Api.Data;
using DatingApp.Api.Entities;
using DatingApp.Api.Extensions;
using DatingApp.Api.Interfaces;
using DatingApp.Api.Middleware;
using DatingApp.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DatingApp.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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

            app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
            
            app.UseAuthentication();
            //tell what you allowed to do.
            app.UseAuthorization();

            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var dataContext = services.GetRequiredService<DataContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                await dataContext.Database.MigrateAsync();
                await Seed.Seedusers(userManager, roleManager);
            }
            catch(Exception ex)
            {
                var logger = services.GetService<ILogger<Program>>();
                logger.LogError(ex, "An Error occurred during migration");
            }
            app.Run();
        }
    }
}
