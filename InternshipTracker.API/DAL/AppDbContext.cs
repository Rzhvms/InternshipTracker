using InternshipTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace InternshipTracker.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Intern> Interns { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<InternshipTrack> InternshipTracks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Intern>()
            .HasIndex(i => i.Email)
            .IsUnique();
            
        modelBuilder.Entity<Intern>()
            .HasIndex(i => i.PhoneNumber)
            .IsUnique()
            .HasFilter("\"PhoneNumber\" IS NOT NULL");
            
        base.OnModelCreating(modelBuilder);
    }
}