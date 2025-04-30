using MediatR;

namespace EventPlanner.Application.Workshops.Commands.DeleteWorkshop;

public class DeleteWorkshopCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
