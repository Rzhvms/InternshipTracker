namespace InternshipTracker.API.Controllers.DTO;

public class InternDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly BirthDate { get; set; }
    public string TrackName { get; set; }
    public string? ProjectName { get; set; }
}