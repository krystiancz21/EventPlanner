using MediatR;

namespace EventPlanner.Application.Reservations.Commands.CancelReservation;

public class CancelReservationCommand : IRequest
{
    public int ReservationId { get; set; }
}