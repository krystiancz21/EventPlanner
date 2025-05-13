using AutoMapper;
using EventPlanner.Application.Workshops.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using EventPlanner.Domain.Repositories;
using EventPlanner.Application.Common;

namespace EventPlanner.Application.Workshops.Queries.GetAllWorkshops;

public class GetAllWorkshopQueryHandler(ILogger<GetAllWorkshopQueryHandler> logger,
    IMapper mapper,
    IWorkshopsRepository workshopsRepository) : IRequestHandler<GetAllWorkshopQuery, PagedResult<WorkshopDto>>
{
    public async Task<PagedResult<WorkshopDto>> Handle(GetAllWorkshopQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all workshops");
        var (workshops, totalCount) = await workshopsRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageNumber, 
            request.PageSize,
            request.SortBy,
            request.SortDirection);

        var workshopDtos = mapper.Map<IEnumerable<WorkshopDto>>(workshops);
        var result = new PagedResult<WorkshopDto>(workshopDtos, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}
