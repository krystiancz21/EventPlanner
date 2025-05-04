using EventPlanner.Domain.Constants;
using MediatR;

namespace EventPlanner.Application.Reservations.Commands.CreateReservation;
public class CreateReservationCommand : IRequest<int>
{
    public int WorkshopId { get; set; } = 0;

    public string UserId { get; set; } = string.Empty;

    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
}
