using EventPlanner.Application.Reservations.Dtos;
using MediatR;

namespace EventPlanner.Application.Reservations.Queries.GetAllReservations;

public class GetAllReservationsQuery : IRequest<IEnumerable<ReservationDto>>
{
}