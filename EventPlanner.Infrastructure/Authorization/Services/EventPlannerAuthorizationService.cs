using EventPlanner.Application.Users;
using EventPlanner.Domain.Constants;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Infrastructure.Authorization.Services;
public class EventPlannerAuthorizationService(ILogger<EventPlannerAuthorizationService> logger,
    IUserContext userContext) : IEventPlannerAuthorizationService
{
    public bool Authorize(Workshop workshop, ResourceOperation resourceOperation)
    {        
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Authorizing user {userEmail}, to {Operation} for workshop {WorkshopName}",
            user.Email,
            resourceOperation,
            workshop.Title);

        if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
        {
            logger.LogInformation("Create/Read operation - successful authorization");
            return true;
        }

        if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
        {
            logger.LogInformation("Admin user, Delete operation - successful authorization");
            return true;
        }

        if ((resourceOperation == ResourceOperation.Update || resourceOperation == ResourceOperation.Delete)
            && user.Id == workshop.OrganizerId)
        {
            logger.LogInformation("Workshop owner - successful authorization");
            return true;
        }

        return false;
    }
}
