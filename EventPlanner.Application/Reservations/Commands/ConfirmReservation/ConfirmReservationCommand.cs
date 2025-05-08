using MediatR;

namespace EventPlanner.Application.Reservations.Commands.ConfirmReservation;

public class ConfirmReservationCommand : IRequest
{
    public int ReservationId { get; set; }
}
