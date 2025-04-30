using MediatR;

namespace EventPlanner.Application.Workshops.Commands.UpdateWorkshop;

public class UpdateWorkshopCommand : IRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int Capacity { get; set; }
}
