
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using myapp_customerwebapp_azure.Application.Interfaces.Repositories;
using myapp_customerwebapp_azure.Application.Interfaces.Services;
using myapp_customerwebapp_azure.Application.Services;
using myapp_customerwebapp_azure.Infrastructure;
using myapp_customerwebapp_azure.Infrastructure.Data;
using myapp_customerwebapp_azure.Infrastructure.Repositories;
using myapp_customerwebapp_azure.Shared.Security;
using Serilog;
using System.Text;

namespace myapp_customerwebapp_azure.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logPath = Path.Combine(
    Environment.GetEnvironmentVariable("HOME") ?? ".",
    "LogFiles",
    "myapp",
    "log-.txt");

            Directory.CreateDirectory(Path.GetDirectoryName(logPath));

            Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.WithThreadId()
    .WriteTo.File(
        path: logPath,
        rollingInterval: RollingInterval.Hour,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} | ThreadId: {ThreadId} | {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateBootstrapLogger(); // Use bootstrap logger to catch early logs

            var builder = WebApplication.CreateBuilder(args);

            // Replace default logging with Serilog
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .MinimumLevel.Debug()
                    .Enrich.WithThreadId()
                    .WriteTo.File(
                        path: "Logs/log-.txt",
                        rollingInterval: RollingInterval.Hour,
                        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} | ThreadId: {ThreadId} | {Level:u3}] {Message:lj}{NewLine}{Exception}"
                    );
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://mytestapp-a3byb0auhje7b5av.southeastasia-01.azurewebsites.net/",
            ValidAudience = "https://mytestapp-a3byb0auhje7b5av.southeastasia-01.azurewebsites.net/",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("3f#8v!bZx9@Lp2&Nj7qRw6$YgTmD1FsUc"))
        };
    });
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<CustomerlyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging()
    .LogTo(Log.Logger.Information,                    // Log to Serilog
                new[] { DbLoggerCategory.Database.Command.Name,
                        DbLoggerCategory.Database.Connection.Name },
                LogLevel.Error,                              // Only errors
                DbContextLoggerOptions.SingleLine
                )    //  shows full values
           .LogTo(Console.WriteLine, LogLevel.Information));
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            // Add services to the container

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseStaticFiles();
            app.MapStaticAssets();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
