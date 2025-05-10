using EventPlanner.Domain.Entities;

namespace EventPlanner.Domain.Interfaces;
public interface ICertificateGenerator
{
    Task<string> GenerateCertificateAsync(Certificate certificate, Workshop workshop, User user); 
    // Returns the URL or path to the generated certificate
}