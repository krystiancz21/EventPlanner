using Microsoft.AspNetCore.Identity;

namespace EventPlanner.Domain.Entities;

public class User : IdentityUser
{
    // ROLE: Organizer / Admin

    public List<Workshop> OwnedWorkshops { get; set; } = [];
    public List<Reservation> Reservations { get; set; } = [];
}
