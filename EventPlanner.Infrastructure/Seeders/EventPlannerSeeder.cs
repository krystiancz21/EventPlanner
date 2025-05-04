using EventPlanner.Domain.Constants;
using EventPlanner.Domain.Entities;
using EventPlanner.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Infrastructure.Seeders;

internal class WorkshopSeeder(EventPlannerDbContext dbContext) : IEventPlannerSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Workshops.Any())
            {
                var workshops = GetWorkshops();
                dbContext.Workshops.AddRange(workshops);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
            [
                new (UserRoles.User)
                {
                    NormalizedName = UserRoles.User.ToUpper(),
                },
                new (UserRoles.Owner)
                {
                    NormalizedName = UserRoles.Owner.ToUpper(),
                },
                new (UserRoles.Admin)
                {
                    NormalizedName = UserRoles.Admin.ToUpper(),
                },
            ];

        return roles;
    }

    private IEnumerable<Workshop> GetWorkshops()
    {
        List<Workshop> workshops =
        [
            new Workshop
            {
                Title = "Workshop 1",
                Description = "Description for Workshop 1",
                Location = "Location 1",
                Date = DateTime.Now.AddDays(30),
                Capacity = 20,
                OrganizerId = "12345"
            },
            new Workshop
            {
                Title = "Workshop 2",
                Description = "Description for Workshop 2",
                Location = "Location 2",
                Date = DateTime.Now.AddDays(60),
                Capacity = 30,
                OrganizerId = "12345"
            }
        ];

        return workshops;
    }
}
