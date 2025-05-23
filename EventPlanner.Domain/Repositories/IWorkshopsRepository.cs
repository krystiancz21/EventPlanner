using EventPlanner.Domain.Constants;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Repositories;

public interface IWorkshopsRepository
{
    Task<IEnumerable<Workshop>> GetAllAsync();
    Task<Workshop?> GetByIdAsync(int id);
    Task<int> Create(Workshop entity);
    Task Delete(Workshop entity);
    Task SaveChanges();
    Task<(IEnumerable<Workshop>, int)> GetAllMatchingAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    Task<(IEnumerable<Workshop>, int)> GetWorkshopsByOwnerId(string userId, string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
}
