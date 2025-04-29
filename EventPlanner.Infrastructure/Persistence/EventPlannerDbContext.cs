using EventPlanner.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventPlanner.Infrastructure.Persistence;

internal class EventPlannerDbContext(DbContextOptions<EventPlannerDbContext> options)
   : IdentityDbContext<User>(options)
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Workshop> Workshops { get; set; }
    internal DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasMany(u => u.OwnedWorkshops)
            .WithOne(w => w.Organizer)
            .HasForeignKey(w => w.OrganizerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Reservations)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Workshop>()
            .HasMany(w => w.Reservations)
            .WithOne(r => r.Workshop)
            .HasForeignKey(r => r.WorkshopId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
