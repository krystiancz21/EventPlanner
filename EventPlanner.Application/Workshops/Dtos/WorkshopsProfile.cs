using AutoMapper;
using EventPlanner.Application.Workshops.Commands.CreateWorkshop;
using EventPlanner.Domain.Entities;

namespace EventPlanner.Application.Workshops.Dtos;

public class WorkshopsProfile : Profile
{
    public WorkshopsProfile()
    {
        CreateMap<Workshop, WorkshopDto>();

        CreateMap<CreateWorkshopCommand, Workshop>();
    }
}
