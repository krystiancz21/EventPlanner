using EventPlanner.Application.Users.Commands.AssignUserRole;
using EventPlanner.Application.Users.Commands.UnassignUserRole;
using EventPlanner.Application.Users.Commands.UpdateUserDetails;
using EventPlanner.Application.Users.Dtos;
using EventPlanner.Application.Users.Queries.GetUserRole;
using EventPlanner.Application.Workshops.Dtos;
using EventPlanner.Application.Workshops.Queries.GetWorkshopById;
using EventPlanner.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("user")]
    [Authorize]
    public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("userRole")]
    [Authorize(Roles = UserRoles.Admin)]
    public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpGet("role")]
    [Authorize]
    public async Task<ActionResult<UserRoleDto>> GetUserRole([FromQuery] GetUserRoleQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }
}
