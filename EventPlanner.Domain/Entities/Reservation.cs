namespace EventPlanner.Domain.Entities;

public class Reservation
{
    public int Id { get; set; }

    public int WorkshopId { get; set; }
    public Workshop Workshop { get; set; } = default!;

    public string UserId { get; set; }
    public User User { get; set; } = default!;

    public string Status { get; set; } = "Pending";
    public DateTime ReservedAt { get; set; } = DateTime.UtcNow;
}
