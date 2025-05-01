using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Interfaces;
using EventPlanner.Domain.Repositories;
using EventPlanner.Infrastructure.Authorization;
using EventPlanner.Infrastructure.Authorization.Requirements;
using EventPlanner.Infrastructure.Authorization.Services;
using EventPlanner.Infrastructure.Persistence;
using EventPlanner.Infrastructure.Repositories;
using EventPlanner.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
            .AddRoles<IdentityRole>()
            .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<EventPlannerDbContext>();

        services.AddScoped<IWorkshopSeeder, WorkshopSeeder>();
        services.AddScoped<IWorkshopsRepository, WorkshopsRepository>();

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicyNames.HasNationality,
                builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Polish"))
            .AddPolicy(PolicyNames.AtLeast18,
                builder => builder.AddRequirements(new MinimumAgeRequirement(18)))
            .AddPolicy(PolicyNames.CreatedAtLeast2Events,
                builder => builder.AddRequirements(new CreatedMultipleEventRequirement(2)));

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, CreatedMultipleEventsRequirementHandler>();
        services.AddScoped<IEventPlannerAuthorizationService, EventPlannerAuthorizationService>();
    }
}
