using AutoMapper;
using EventPlanner.Application.Workshops.Dtos;
using EventPlanner.Application.Workshops.Queries.GetAllWorkshops;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Workshops.Queries.GetWorkshopById;

public class GetWorkshopByIdQueryHandler(ILogger<GetAllWorkshopQueryHandler> logger,
    IMapper mapper,
    IWorkshopsRepository workshopsRepository) : IRequestHandler<GetWorkshopByIdQuery, WorkshopDto>
{
    public async Task<WorkshopDto> Handle(GetWorkshopByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting workshop by id {WorkshopId}", request.Id);
        var workshop = await workshopsRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Workshops), request.Id.ToString());
        var workshopDto = mapper.Map<WorkshopDto>(workshop);

        return workshopDto;
    }
}
