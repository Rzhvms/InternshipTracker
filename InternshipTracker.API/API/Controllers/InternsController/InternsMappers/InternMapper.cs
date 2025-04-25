using InternshipTracker.API.Controllers.DTO;
using InternshipTracker.API.Models;

namespace InternshipTracker.API.Controllers.DTO;

public static class InternMapper
{
    public static InternDto ToDto(Intern intern) => new InternDto
    {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        Gender = intern.Gender,
        Email = intern.Email,
        PhoneNumber = intern.PhoneNumber,
        BirthDate = intern.BirthDate,
        TrackName = intern.Track?.Name ?? "",
        ProjectName = intern.Project?.Name
    };

    public static Intern ToEntity(CreateInternDto dto) => new Intern
    {
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Gender = dto.Gender,
        Email = dto.Email,
        PhoneNumber = dto.PhoneNumber,
        BirthDate = dto.BirthDate,
        TrackId = dto.TrackId,
        ProjectId = dto.ProjectId,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    public static void UpdateEntity(Intern existing, CreateInternDto dto)
    {
        existing.FirstName = dto.FirstName;
        existing.LastName = dto.LastName;
        existing.Gender = dto.Gender;
        existing.Email = dto.Email;
        existing.PhoneNumber = dto.PhoneNumber;
        existing.BirthDate = dto.BirthDate;
        existing.TrackId = dto.TrackId;
        existing.ProjectId = dto.ProjectId;
        existing.UpdatedAt = DateTime.UtcNow;
    }
}