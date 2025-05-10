using EventPlanner.Application.Certificates.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.API.Controllers;

[ApiController]
[Route("api/certificates")]
[Authorize]
public class CertificatesController(IMediator mediator) : ControllerBase
{
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateCertificate([FromBody] GenerateCertificateCommand command)
    {
        int certificateId = await mediator.Send(command);
        return Ok(new { CertificateId = certificateId });
    }
}
