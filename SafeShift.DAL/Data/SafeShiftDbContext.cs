using Microsoft.EntityFrameworkCore;
using SafeShift.Models;

namespace SafeShift.DAL.Data;

public class SafeShiftDbContext : DbContext
{
    public SafeShiftDbContext(DbContextOptions<SafeShiftDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<Inspection> Inspections => Set<Inspection>();
    public DbSet<Shift> Shifts => Set<Shift>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.UserId);

            entity.Property(user => user.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasIndex(user => user.Email)
                .IsUnique();

            entity.Property(user => user.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            entity.Property(user => user.Role)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasMany(user => user.Incidents)
                .WithOne(incident => incident.User)
                .HasForeignKey(incident => incident.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(user => user.Inspections)
                .WithOne(inspection => inspection.User)
                .HasForeignKey(inspection => inspection.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(user => user.Shifts)
                .WithOne(shift => shift.User)
                .HasForeignKey(shift => shift.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Incident>(entity =>
        {
            entity.HasKey(incident => incident.IncidentId);

            entity.Property(incident => incident.Description)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(incident => incident.Date)
                .IsRequired();

            entity.Property(incident => incident.Severity)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Inspection>(entity =>
        {
            entity.HasKey(inspection => inspection.InspectionId);

            entity.Property(inspection => inspection.Date)
                .IsRequired();

            entity.Property(inspection => inspection.Notes)
                .IsRequired()
                .HasMaxLength(1000);
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(shift => shift.ShiftId);

            entity.Property(shift => shift.StartTime)
                .IsRequired();

            entity.Property(shift => shift.EndTime)
                .IsRequired();
        });
    }
}
