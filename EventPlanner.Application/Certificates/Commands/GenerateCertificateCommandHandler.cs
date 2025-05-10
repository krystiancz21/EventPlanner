using EventPlanner.Domain.Entities;
using EventPlanner.Domain.Exceptions;
using EventPlanner.Domain.Interfaces;
using EventPlanner.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Application.Certificates.Commands;

public class GenerateCertificateCommandHandler(
    ILogger<GenerateCertificateCommandHandler> logger,
    IWorkshopsRepository workshopRepository,
    UserManager<User> userManager,
    ICertificateRepository certificateRepository,
    ICertificateGenerator certificateGenerator) : IRequestHandler<GenerateCertificateCommand, int>
{
    public async Task<int> Handle(GenerateCertificateCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Generating certificate for WorkshopId: {WorkshopId}, UserId: {UserId}", request.WorkshopId, request.UserId);

        var workshop = await workshopRepository.GetByIdAsync(request.WorkshopId)
            ?? throw new NotFoundException(nameof(Workshop), request.WorkshopId.ToString());

        var user = await userManager.FindByIdAsync(request.UserId)
            ?? throw new NotFoundException(nameof(User), request.UserId);

        var certificate = new Certificate
        {
            WorkshopId = request.WorkshopId,
            UserId = request.UserId,
            Name = $"Certificate for {workshop.Title}", // Customize as needed
            Description = $"Certificate of completion for attending {workshop.Title}", // Customize
        };

        certificate.CertificateURL = await certificateGenerator.GenerateCertificateAsync(certificate, workshop, user);

        int certificateId = await certificateRepository.CreateAsync(certificate);

        logger.LogInformation("Certificate generated with ID: {CertificateId}", certificateId);
        return certificateId;
    }
}