using EventPlanner.Application.Users.Dtos;
using MediatR;

namespace EventPlanner.Application.Users.Queries.GetUserRole
{
    public class GetUserRoleQuery : IRequest<UserRoleDto>
    {
    }
}
