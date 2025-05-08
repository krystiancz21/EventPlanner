using EventPlanner.Application.Users;
using EventPlanner.Domain.Constants;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Reservations.Commands.ConfirmReservation;

public class ConfirmReservationCommandHandler(
    ILogger<ConfirmReservationCommandHandler> logger,
    IReservationsRepository reservationsRepository,
    IUserContext userContext) : IRequestHandler<ConfirmReservationCommand>
{
    public async Task Handle(ConfirmReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Confirming reservation with id {Id}", request.ReservationId);
        var currentUser = userContext.GetCurrentUser();

        var reservation = await reservationsRepository.GetByIdAsync(request.ReservationId)
            ?? throw new NotFoundException(nameof(Reservation), request.ReservationId.ToString());

        if (reservation.UserId != currentUser.Id)
            throw new ForbidException();

        if (reservation.Status != ReservationStatus.Pending)
            throw new InvalidOperationException("Reservation is not pending");

        reservation.Status = ReservationStatus.Confirmed;

        await reservationsRepository.SaveChanges();
    }
}
