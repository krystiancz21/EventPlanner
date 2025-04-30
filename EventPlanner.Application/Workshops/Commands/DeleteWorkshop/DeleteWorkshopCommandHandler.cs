using AutoMapper;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Workshops.Commands.DeleteWorkshop;

public class DeleteWorkshopCommandHandler(ILogger<DeleteWorkshopCommandHandler> logger,
    IWorkshopsRepository workshopsRepository) : IRequestHandler<DeleteWorkshopCommand>
{
    public async Task Handle(DeleteWorkshopCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting a workshop with id {WorkshopId}", request.Id);

        var workshop = await workshopsRepository.GetByIdAsync(request.Id);
        if (workshop is null)
            throw new NotFoundException(nameof(Workshop), request.Id.ToString());

        await workshopsRepository.Delete(workshop);
    }
}
