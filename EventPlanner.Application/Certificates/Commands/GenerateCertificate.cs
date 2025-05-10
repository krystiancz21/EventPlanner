using MediatR;

namespace EventPlanner.Application.Certificates.Commands;
public class GenerateCertificateCommand : IRequest<int>
{
    public int WorkshopId { get; set; }
    public string UserId { get; set; }
}