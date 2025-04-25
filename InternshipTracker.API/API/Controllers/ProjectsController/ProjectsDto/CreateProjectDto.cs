using System.ComponentModel.DataAnnotations;

namespace InternshipTracker.API.Controllers.DTO;

public class CreateProjectDto
{
    [Required]
    public string Name { get; set; }
}