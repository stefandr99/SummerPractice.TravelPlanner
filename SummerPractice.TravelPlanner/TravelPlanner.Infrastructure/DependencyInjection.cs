namespace TravelPlanner.Infrastructure;

using Data;
using Domain.Common;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructuresService(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(appSettings.ConnectionStrings.DefaultConnection));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}