using AutoMapper;
using EventPlanner.Application.Users;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler(ILogger<CreateReservationCommandHandler> logger,
    IMapper mapper,
    IReservationsRepository reservationsRepository,
    IUserContext userContext) : IRequestHandler<CreateReservationCommand, int>
{
    public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("Creating a new reservation {@Reservation}", request);

        var reservation = mapper.Map<Reservation>(request);
        reservation.UserId = currentUser.Id;

        int reservationId = await reservationsRepository.Create(reservation);

        return reservationId;
    }
}
