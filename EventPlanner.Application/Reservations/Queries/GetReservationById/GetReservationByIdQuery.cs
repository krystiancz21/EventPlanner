using EventPlanner.Application.Reservations.Dtos;
using MediatR;

namespace EventPlanner.Application.Reservations.Queries.GetReservationById;

public class GetReservationByIdQuery(int id) : IRequest<ReservationDto>
{
    public int Id { get; } = id;
}
