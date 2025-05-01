using AutoMapper;
using EventPlanner.Application.Workshops.Commands.CreateWorkshop;
using EventPlanner.Application.Workshops.Queries.GetAllWorkshops;
using EventPlanner.Domain.Constants;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Interfaces;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Workshops.Commands.UpdateWorkshop;

public class UpdateWorkshopCommandHandler(ILogger<UpdateWorkshopCommandHandler> logger,
    IMapper mapper,
    IWorkshopsRepository workshopsRepository,
    IEventPlannerAuthorizationService eventPlannerAuthorizationService) : IRequestHandler<UpdateWorkshopCommand>
{
    public async Task Handle(UpdateWorkshopCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating workshop with id {Id} {@Workshop}", request.Id, request);

        var workshop = await workshopsRepository.GetByIdAsync(request.Id)
        ?? throw new NotFoundException(nameof(Workshop), request.Id.ToString());

        if (!eventPlannerAuthorizationService.Authorize(workshop, ResourceOperation.Update))
            throw new ForbidException();

        mapper.Map(request, workshop);

        await workshopsRepository.SaveChanges();
    }
}
