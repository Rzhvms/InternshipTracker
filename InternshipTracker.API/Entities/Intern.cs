using System.ComponentModel.DataAnnotations;

namespace InternshipTracker.API.Entities;

public class Intern
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    [MaxLength(50)]
    public required string FirstName { get; set; }
    
    [MaxLength(50)]
    public required string LastName { get; set; }
    
    [MaxLength(255)]
    [EmailAddress]
    public required string Email { get; set; }
    
    [MaxLength(20)]
    [Phone]
    public string? Phone { get; set; }
    
    [DataType(DataType.Date)]
    public required DateTime BirthDate { get; set; }
    
    public required  Guid TrackId { get; init; }
    public InternshipTrack? Track { get; set; }
    
    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; }
}