using AutoMapper;
using EventPlanner.Application.Common;
using EventPlanner.Application.Users;
using EventPlanner.Application.Workshops.Dtos;
using EventPlanner.Application.Workshops.Queries.GetAllWorkshops;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Workshops.Queries.GetMyWorkshops;

public class GetMyWorkshopsQueryHandler(ILogger<GetMyWorkshopsQueryHandler> logger,
    IMapper mapper,
    IWorkshopsRepository workshopsRepository,
    IUserContext userContext) : IRequestHandler<GetMyWorkshopsQuery, PagedResult<WorkshopDto>>
{    public async Task<PagedResult<WorkshopDto>> Handle(GetMyWorkshopsQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        
        logger.LogInformation("Getting all workshops for owner {userId}", currentUser.Id);
        var (workshops, totalCount) = await workshopsRepository.GetWorkshopsByOwnerId(currentUser.Id,
            request.SearchPhrase,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        var workshopDtos = mapper.Map<IEnumerable<WorkshopDto>>(workshops);
        var result = new PagedResult<WorkshopDto>(workshopDtos, totalCount, request.PageSize, request.PageNumber);
        return result;
    }
}