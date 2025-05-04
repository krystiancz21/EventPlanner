using EventPlanner.Domain.Constants;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.Reservations.Dtos;

public class ReservationDto
{
    public int Id { get; set; }

    public int WorkshopId { get; set; }

    public string UserId { get; set; }

    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
}
