using InternshipTracker.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternshipTracker.API.Data;

public class AppDbContext : DbContext
{
    public DbSet<Intern> Interns { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<InternshipTrack> InternshipTracks { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}