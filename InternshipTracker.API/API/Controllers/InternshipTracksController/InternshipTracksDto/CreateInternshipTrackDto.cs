using System.ComponentModel.DataAnnotations;

namespace InternshipTracker.API.Controllers.DTO;

public class CreateInternshipTrackDto
{
    [Required]
    public string Name { get; set; }
}