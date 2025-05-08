using EventPlanner.Application.Reservations.Commands.ConfirmReservation;
using EventPlanner.Application.Users;
using EventPlanner.Domain.Constants;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Reservations.Commands.CancelReservation;

public class CancelReservationCommandHandler(
    ILogger<CancelReservationCommandHandler> logger,
    IReservationsRepository reservationsRepository,
    IUserContext userContext) : IRequestHandler<CancelReservationCommand>
{
    public async Task Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Canceling reservation with id {Id}", request.ReservationId);
        var currentUser = userContext.GetCurrentUser();

        var reservation = await reservationsRepository.GetByIdAsync(request.ReservationId)
            ?? throw new NotFoundException(nameof(Reservation), request.ReservationId.ToString());

        if (reservation.UserId != currentUser.Id)
            throw new ForbidException();

        //if (reservation.Status != ReservationStatus.Pending)
        //    throw new InvalidOperationException("Reservation is not pending");

        reservation.Status = ReservationStatus.Cancelled;

        await reservationsRepository.SaveChanges();
    }
}
