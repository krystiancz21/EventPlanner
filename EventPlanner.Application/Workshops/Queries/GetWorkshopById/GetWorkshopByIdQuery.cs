using EventPlanner.Application.Workshops.Dtos;
using MediatR;

namespace EventPlanner.Application.Workshops.Queries.GetWorkshopById;

public class GetWorkshopByIdQuery(int id) : IRequest<WorkshopDto>
{
    public int Id { get; } = id;
}
