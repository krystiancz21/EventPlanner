using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Repositories;

public interface IReservationsRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<int> Create(Reservation entity);
    Task SaveChanges();
}