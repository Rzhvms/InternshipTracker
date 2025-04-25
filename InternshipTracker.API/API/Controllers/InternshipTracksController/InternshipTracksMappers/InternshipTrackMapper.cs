using InternshipTracker.API.Models;

namespace InternshipTracker.API.Controllers.DTO;

public static class InternshipTrackMapper
{
    public static InternshipTrackDto ToDto(InternshipTrack track) => new InternshipTrackDto
    {
        Id = track.Id,
        Name = track.Name
    };

    public static InternshipTrack ToEntity(CreateInternshipTrackDto dto) => new InternshipTrack
    {
        Name = dto.Name,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    public static void UpdateEntity(InternshipTrack existing, CreateInternshipTrackDto dto)
    {
        existing.Name = dto.Name;
        existing.UpdatedAt = DateTime.UtcNow;
    }
}