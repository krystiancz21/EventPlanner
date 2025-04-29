namespace EventPlanner.Domain.Entities;

public class Workshop
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; }
    public string Location { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int Capacity { get; set; }

    public string OrganizerId { get; set; } = default!;
    public User Organizer { get; set; } = default!;

    public List<Reservation> Reservations { get; set; } = [];
}
