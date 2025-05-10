namespace EventPlanner.Application.Certificates.Dtos;

public class CertificateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? CertificateURL { get; set; }
    public DateTime GeneratedAt { get; set; }
}
