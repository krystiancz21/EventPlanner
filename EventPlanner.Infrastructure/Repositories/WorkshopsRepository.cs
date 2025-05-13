using System.Linq.Expressions;
using EventPlanner.Domain.Constants;
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

    public async Task<(IEnumerable<Workshop>, int)> GetAllMatchingAsync(string? searchPhrase, 
        int pageNumber,
        int pageSize, 
        string? sortBy,
        SortDirection sortDirection)
    {
        var searchPhraseLower = searchPhrase?.ToLower();

        var baseQuery = dbContext
            .Workshops
            .Where(w => searchPhraseLower == null ||
                        (w.Title.ToLower().Contains(searchPhraseLower) ||
                        w.Description.ToLower().Contains(searchPhraseLower)));

        var totalCount = await baseQuery.CountAsync();

        if (!string.IsNullOrEmpty(sortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Workshop, object>>>
            {
                { nameof(Workshop.Title), r => r.Title },
                { nameof(Workshop.Description), r => r.Description },
            };

            var selectedColumn = columnsSelector[sortBy];

            baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var workshops = await baseQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (workshops, totalCount);
    }

    public async Task<Workshop?> GetByIdAsync(int id)
    {
        var workshop = await dbContext.Workshops.FirstOrDefaultAsync(w => w.Id == id);
        return workshop;
    }

    public Task SaveChanges()
    => dbContext.SaveChangesAsync();
}
