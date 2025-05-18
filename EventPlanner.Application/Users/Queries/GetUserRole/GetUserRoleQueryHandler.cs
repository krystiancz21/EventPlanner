using MediatR;
using EventPlanner.Application.Users.Dtos;
using EventPlanner.Application.Reservations.Queries.GetReservationById;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Users.Queries.GetUserRole
{
    public class GetUserRoleQueryHandler(ILogger<GetUserRoleQueryHandler> logger, 
        IUserContext userContext) : IRequestHandler<GetUserRoleQuery, UserRoleDto>
    {
        public async Task<UserRoleDto> Handle(GetUserRoleQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting user role");

            var currentUser = userContext.GetCurrentUser();
            
            if (currentUser == null)
            {
                throw new UnauthorizedAccessException("User is not authenticated");
            }
            
            return new UserRoleDto(
                currentUser.Id, 
                currentUser.Email, 
                currentUser.Roles
            );
        }
    }
}