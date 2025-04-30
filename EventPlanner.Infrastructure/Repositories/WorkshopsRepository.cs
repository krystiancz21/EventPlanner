using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using EventPlanner.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Repositories;

internal class WorkshopsRepository(EventPlannerDbContext dbContext): IWorkshopsRepository
{
    public async Task<int> Create(Workshop entity)
    {
        dbContext.Workshops.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task Delete(Workshop entity)
    {
        dbContext.Workshops.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Workshop>> GetAllAsync()
    {
        var workshops = await dbContext.Workshops.ToListAsync();

        return workshops;
    }

    public async Task<Workshop?> GetByIdAsync(int id)
    {
        var workshop = await dbContext.Workshops.FirstOrDefaultAsync(w => w.Id == id);
        return workshop;
    }

    public Task SaveChanges()
    => dbContext.SaveChangesAsync();
}
