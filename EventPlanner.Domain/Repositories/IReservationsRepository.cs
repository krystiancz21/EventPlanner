using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Repositories;

public interface IReservationsRepository
{
    Task<IEnumerable<Reservation>> GetAllAsync();
    Task<Reservation?> GetByIdAsync(int id);
    Task<int> Create(Reservation entity);
    Task SaveChanges();
    Task<Reservation?> GetByWorkshopIdAndUserId(int workshopId, string userId);
    Task<int> GetReservationCountByWorkshopId(int workshopId);
}