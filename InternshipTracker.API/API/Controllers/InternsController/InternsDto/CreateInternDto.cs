using System.ComponentModel.DataAnnotations;

namespace InternshipTracker.API.Controllers.DTO;

public class CreateInternDto
{
    [Required] 
    public string FirstName { get; set; }
        
    [Required] 
    public string LastName { get; set; }
        
    [Required] 
    public string Gender { get; set; }
        
    [Required]
    [EmailAddress] 
    public string Email { get; set; }
        
    [RegularExpression(@"^\+7\d{10}$")] 
    public string? PhoneNumber { get; set; }
        
    [Required] 
    public DateOnly BirthDate { get; set; }
        
    [Required] 
    public int TrackId { get; set; }
        
    public int? ProjectId { get; set; }
}