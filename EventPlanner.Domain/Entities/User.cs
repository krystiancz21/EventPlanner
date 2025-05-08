using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Domain.Entities;

public class User : IdentityUser
{
    // ROLE: Organizer / Admin
    public DateOnly? DateOfBirth { get; set; }
    public string? Nationality { get; set; }

    public List<Workshop> OwnedWorkshops { get; set; } = [];
    public List<Reservation> Reservations { get; set; } = [];
    public List<Certificate> Certificates { get; set; } = [];
}
