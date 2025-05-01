using EventPlanner.Application.Users;
using EventPlanner.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace EventPlanner.Infrastructure.Authorization.Requirements;

internal class CreatedMultipleEventsRequirementHandler(IWorkshopsRepository eventsRepository,
    IUserContext userContext) : AuthorizationHandler<CreatedMultipleEventRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        CreatedMultipleEventRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        var events = await eventsRepository.GetAllAsync();

        var userRestaurantsCreated = events.Count(r => r.OrganizerId == currentUser!.Id);

        if (userRestaurantsCreated >= requirement.MinimumEventsCreated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
