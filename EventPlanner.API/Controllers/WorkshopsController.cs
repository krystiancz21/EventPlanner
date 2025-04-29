using EventPlanner.Application.Workshops.Commands.CreateWorkshop;
using EventPlanner.Application.Workshops.Queries.GetAllWorkshops;
using EventPlanner.Application.Workshops.Queries.GetWorkshopById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers;

[ApiController]
[Route("api/workshops")]
public class WorkshopsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var workshops = await mediator.Send(new GetAllWorkshopQuery());
        return Ok(workshops);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var workshop = await mediator.Send(new GetWorkshopByIdQuery(id));
        return Ok(workshop);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkshop([FromBody] CreateWorkshopCommand command)
    {
        var workshopId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = workshopId }, null);
    }

    //[HttpPatch("{id}")]
    //public async Task<IActionResult> UpdateWorkshop([FromRoute] int id, UpdateWorkshopCommand command)
    //{
    //    command.Id = id;
    //    await mediator.Send(command);

    //    return NoContent();
    //}

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteWorkshop([FromRoute] int id)
    //{
    //    await mediator.Send(new DeleteWorkshopCommand(id));

    //    return NoContent();
    //}
}
