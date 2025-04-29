using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Repositories;

public interface IWorkshopsRepository
{
    Task<IEnumerable<Workshop>> GetAllAsync();
    Task<Workshop?> GetByIdAsync(int id);
    Task<int> Create(Workshop entity);
}
