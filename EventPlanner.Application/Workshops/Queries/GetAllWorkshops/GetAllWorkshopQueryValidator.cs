using EventPlanner.Application.Workshops.Dtos;
using FluentValidation;

namespace EventPlanner.Application.Workshops.Queries.GetAllWorkshops;

internal class GetAllWorkshopQueryValidator : AbstractValidator<GetAllWorkshopQuery>
{
    private int[] allowPageSizes = [ 5, 10, 15, 30 ];
    private string[] allowedSortByColumnNames = [nameof(WorkshopDto.Title), 
        nameof(WorkshopDto.Description)];

    public GetAllWorkshopQueryValidator()
    {
        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPageSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumnNames.Contains(value))
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}
