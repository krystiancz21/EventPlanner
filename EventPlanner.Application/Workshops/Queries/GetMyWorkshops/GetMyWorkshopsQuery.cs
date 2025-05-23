using EventPlanner.Application.Common;
using EventPlanner.Application.Workshops.Dtos;
using EventPlanner.Domain.Constants;
using MediatR;

namespace EventPlanner.Application.Workshops.Queries.GetMyWorkshops;

public class GetMyWorkshopsQuery : IRequest<PagedResult<WorkshopDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}

