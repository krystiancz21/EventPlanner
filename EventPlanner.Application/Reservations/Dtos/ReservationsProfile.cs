using AutoMapper;
using EventPlanner.Application.Reservations.Commands.CreateReservation;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.Reservations.Dtos;

public class ReservationsProfile : Profile
{
    public ReservationsProfile()
    {
        CreateMap<Reservation, ReservationDto>();
        CreateMap<CreateReservationCommand, Reservation>();
    }
}

