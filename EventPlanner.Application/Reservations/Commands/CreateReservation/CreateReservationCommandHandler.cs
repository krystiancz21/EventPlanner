using AutoMapper;
using EventPlanner.Application.Users;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler(ILogger<CreateReservationCommandHandler> logger,
    IMapper mapper,
    IReservationsRepository reservationsRepository,
    IWorkshopsRepository workshopRepository,
    IUserContext userContext) : IRequestHandler<CreateReservationCommand, int>
{
    public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("Creating a new reservation {@Reservation}", request);

        var workshop = await workshopRepository.GetByIdAsync(request.WorkshopId)
            ?? throw new NotFoundException(nameof(Workshop), request.WorkshopId.ToString());

        if (workshop.Date < DateTime.UtcNow.Date)
        {
            throw new BadRequestException(nameof(Workshop), request.WorkshopId.ToString(), "Cannot create a reservation for a past workshop");
        }

        var reservationCount = await reservationsRepository.GetReservationCountByWorkshopId(request.WorkshopId);

        if (workshop.Capacity <= reservationCount)
            throw new BadRequestException(nameof(Workshop), request.WorkshopId.ToString(), "Workshop is full");

        var existingReservation = await reservationsRepository.GetByWorkshopIdAndUserId(request.WorkshopId, currentUser.Id);
        if (existingReservation != null)
        {
            throw new BadRequestException(nameof(Workshop), request.WorkshopId.ToString(), "You already have a reservation for this workshop");
        }

        var reservation = mapper.Map<Reservation>(request);
        reservation.UserId = currentUser.Id;

        int reservationId = await reservationsRepository.Create(reservation);

        return reservationId;
    }
}
