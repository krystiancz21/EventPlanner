using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using EventPlanner.Infrastructure.Persistence;
using EventPlanner.Infrastructure.Repositories;
using EventPlanner.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventPlanner.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EventPlannerDb");

        services.AddDbContext<EventPlannerDbContext>(options => 
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<EventPlannerDbContext>();
            //.AddRoles<IdentityRole>()
            //.AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()

        services.AddScoped<IWorkshopSeeder, WorkshopSeeder>();
        services.AddScoped<IWorkshopsRepository, WorkshopsRepository>();
    }
}
