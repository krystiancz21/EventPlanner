using EventPlanner.Application.Workshops.Dtos;
using MediatR;

namespace EventPlanner.Application.Workshops.Queries.GetAllWorkshops;

public class GetAllWorkshopQuery : IRequest<IEnumerable<WorkshopDto>>
{
}
