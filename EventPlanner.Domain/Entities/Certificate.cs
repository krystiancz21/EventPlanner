namespace EventPlanner.Domain.Entities;

public class Certificate
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public int WorkshopId { get; set; } = default!;
    public Workshop Workshop { get; set; } = default!;

    public string UserId { get; set; } = default!;
    public User User { get; set; } = default!;

    public string? CertificateURL { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
}
