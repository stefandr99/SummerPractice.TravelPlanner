namespace TravelPlanner.Infrastructure;

using Data;
using Domain.Common;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using System.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, AppSettings appSettings)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment == "Testing")
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(appSettings.ConnectionStrings.DefaultConnection));
        }

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}