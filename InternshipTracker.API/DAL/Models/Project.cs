using System.ComponentModel.DataAnnotations;

namespace InternshipTracker.API.Models;

public class Project
{
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public ICollection<Intern> Interns { get; set; } = new List<Intern>();
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}