using AutoMapper;
using EventPlanner.Application.Reservations.Dtos;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Reservations.Queries.GetReservationById;

public class GetReservationByIdQueryHandler(ILogger<GetReservationByIdQueryHandler> logger,
    IMapper mapper,
    IReservationsRepository reservationsRepository) : IRequestHandler<GetReservationByIdQuery, ReservationDto>
{
    public async Task<ReservationDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting reservation by id {ReservationId}", request.Id);
        var reservation = await reservationsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Reservation), request.Id.ToString());
        var reservationDto = mapper.Map<ReservationDto>(reservation);

        return reservationDto;
    }
}
