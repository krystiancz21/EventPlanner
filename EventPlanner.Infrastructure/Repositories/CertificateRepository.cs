using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using EventPlanner.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Repositories;

internal class CertificateRepository(EventPlannerDbContext dbContext) : ICertificateRepository
{
    public async Task<Certificate?> GetByIdAsync(int id)
    {
        return await dbContext.Certificates.FindAsync(id);
    }

    public async Task<int> CreateAsync(Certificate certificate)
    {
        dbContext.Certificates.Add(certificate);
        await dbContext.SaveChangesAsync();
        return certificate.Id;
    }

    public async Task<IEnumerable<Certificate>> GetByWorkshopIdAsync(int workshopId)
    {
        return await dbContext.Certificates
            .Where(c => c.WorkshopId == workshopId)
            .ToListAsync();
    }

    public Task SaveChangesAsync()
    => dbContext.SaveChangesAsync();
}
