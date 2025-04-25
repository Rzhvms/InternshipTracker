using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternshipTracker.API.Models;

public class Intern
{
    public int Id { get; set; }
    
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Gender { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [RegularExpression(@"^\+7\d{10}$", ErrorMessage = "Номер телефона должен начинаться с +7 и содержать 10 цифр после")]
    public string? PhoneNumber { get; set; }
    
    [Required]
    public DateOnly BirthDate { get; set; }
    
    [Required]
    public int TrackId { get; set; }
    public InternshipTrack? Track { get; set; }
    
    public int? ProjectId { get; set; }
    public Project? Project { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}