using AutoMapper;
using EventPlanner.Application.Users;
using EventPlanner.Application.Workshops.Queries.GetAllWorkshops;
using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Workshops.Commands.CreateWorkshop;

public class CreateWorkshopCommandHandler(ILogger<CreateWorkshopCommandHandler> logger,
    IMapper mapper,
    IWorkshopsRepository workshopsRepository,
    IUserContext userContext) : IRequestHandler<CreateWorkshopCommand, int>
{
    public async Task<int> Handle(CreateWorkshopCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("Creating a new workshop {@Workshop}", request);

        var workshop = mapper.Map<Workshop>(request);
        workshop.OrganizerId = currentUser.Id;

        int workshopId = await workshopsRepository.Create(workshop);
        
        return workshopId;
    }
}
