using AutoMapper;
using EventPlanner.Application.Workshops.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using EventPlanner.Domain.Repositories;

namespace EventPlanner.Application.Workshops.Queries.GetAllWorkshops;

public class GetAllWorkshopQueryHandler(ILogger<GetAllWorkshopQueryHandler> logger,
    IMapper mapper,
    IWorkshopsRepository workshopsRepository) : IRequestHandler<GetAllWorkshopQuery, IEnumerable<WorkshopDto>>
{
    public async Task<IEnumerable<WorkshopDto>> Handle(GetAllWorkshopQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all workshops");
        var workshops = await workshopsRepository.GetAllAsync();
        var workshopDtos = mapper.Map<IEnumerable<WorkshopDto>>(workshops);
        return workshopDtos;
    }
}
