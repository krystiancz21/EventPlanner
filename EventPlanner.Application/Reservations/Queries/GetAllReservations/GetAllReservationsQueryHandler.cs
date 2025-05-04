using AutoMapper;
using EventPlanner.Application.Reservations.Dtos;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Reservations.Queries.GetAllReservations;

public class GetAllReservationsQueryHandler(ILogger<GetAllReservationsQueryHandler> logger,
    IMapper mapper,
    IReservationsRepository reservationsRepository) : IRequestHandler<GetAllReservationsQuery, IEnumerable<ReservationDto>>
{
    public async Task<IEnumerable<ReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all reservations");
        var reservations = await reservationsRepository.GetAllAsync();
        var reservationDtos = mapper.Map<IEnumerable<ReservationDto>>(reservations);
        return reservationDtos;
    }
}
