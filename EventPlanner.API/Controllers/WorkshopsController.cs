using EventPlanner.Application.Workshops.Commands.CreateWorkshop;
using EventPlanner.Application.Workshops.Commands.DeleteWorkshop;
using EventPlanner.Application.Workshops.Commands.UpdateWorkshop;
using EventPlanner.Application.Workshops.Dtos;
using EventPlanner.Application.Workshops.Queries.GetAllWorkshops;
using EventPlanner.Application.Workshops.Queries.GetWorkshopById;
using EventPlanner.Domain.Constants;
using EventPlanner.Infrastructure.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers;

[ApiController]
[Route("api/workshops")]
[Authorize]
public class WorkshopsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<WorkshopDto>>> GetAll()
    {
        var workshops = await mediator.Send(new GetAllWorkshopQuery());
        return Ok(workshops);
    }

    [HttpGet("{id}")]
    //[Authorize(Policy = PolicyNames.HasNationality)]
    //[Authorize(Policy = PolicyNames.AtLeast18)]
    public async Task<ActionResult<WorkshopDto?>> GetById([FromRoute] int id)
    {
        var workshop = await mediator.Send(new GetWorkshopByIdQuery(id));
        return Ok(workshop);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWorkshop([FromRoute] int id, UpdateWorkshopCommand command)
    {
        command.Id = id;
        await mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWorkshop([FromRoute] int id)
    {
        await mediator.Send(new DeleteWorkshopCommand(id));

        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = UserRoles.Owner)]
    public async Task<IActionResult> CreateWorkshop([FromBody] CreateWorkshopCommand command)
    {
        var workshopId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = workshopId }, null);
    }
}
