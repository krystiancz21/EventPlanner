using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Repositories;

public interface ICertificateRepository
{
    Task<Certificate?> GetByIdAsync(int id);
    Task<int> CreateAsync(Certificate certificate);
    Task<IEnumerable<Certificate>> GetByWorkshopIdAsync(int workshopId);
    Task SaveChangesAsync();
}
