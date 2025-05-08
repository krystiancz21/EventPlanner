using EventPlanner.Application.Reservations.Commands.CancelReservation;
using EventPlanner.Application.Reservations.Commands.ConfirmReservation;
using EventPlanner.Application.Reservations.Commands.CreateReservation;
using EventPlanner.Application.Reservations.Dtos;
using EventPlanner.Application.Reservations.Queries.GetAllReservations;
using EventPlanner.Application.Reservations.Queries.GetReservationById;
using EventPlanner.Application.Workshops.Commands.CreateWorkshop;
using EventPlanner.Application.Workshops.Dtos;
using EventPlanner.Application.Workshops.Queries.GetWorkshopById;
using EventPlanner.Domain.Constants;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers;

[ApiController]
[Route("api/reservations")]
[Authorize]
public class ReservationsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll()
    {
        var reservation = await mediator.Send(new GetAllReservationsQuery());
        return Ok(reservation);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDto?>> GetById([FromRoute] int id)
    {
        var reservation = await mediator.Send(new GetReservationByIdQuery(id));
        return Ok(reservation);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand command)
    {
        var workshopId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = workshopId }, null);
    }

    [HttpPatch("{id}/confirm")]
    public async Task<IActionResult> ComfirmReservation([FromRoute] int id, [FromBody] ConfirmReservationCommand command)
    {
        command.ReservationId = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPatch("{id}/cancel")]
    public async Task<IActionResult> CancelReservation([FromRoute] int id, [FromBody] CancelReservationCommand command)
    {
        command.ReservationId = id;
        await mediator.Send(command);
        return NoContent();
    }
}
