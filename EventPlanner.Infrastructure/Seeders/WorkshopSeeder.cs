using EventPlanner.Domain.Entities;
using EventPlanner.Infrastructure.Persistence;

namespace EventPlanner.Infrastructure.Seeders;

internal class WorkshopSeeder(EventPlannerDbContext dbContext) : IWorkshopSeeder
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
        }
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
