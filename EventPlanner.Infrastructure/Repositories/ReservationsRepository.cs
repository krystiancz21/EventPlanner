using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using EventPlanner.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Repositories;

internal class ReservationsRepository(EventPlannerDbContext dbContext) : IReservationsRepository
{
    public async Task<int> Create(Reservation entity)
    {;
        dbContext.Reservations.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<IEnumerable<Reservation>> GetAllAsync()
    {
        var reservations = await dbContext.Reservations.ToListAsync();
        return reservations;
    }

    public async Task<Reservation?> GetByIdAsync(int id)
    {
        var reservation = await dbContext.Reservations.FirstOrDefaultAsync(w => w.Id == id);
        return reservation;
    }
}
