using InternshipTracker.API.Models;

namespace InternshipTracker.API.Controllers.DTO;

public static class ProjectMapper
{
    public static ProjectDto ToDto(Project p) => new ProjectDto { Id = p.Id, Name = p.Name };

    public static Project ToEntity(CreateProjectDto dto) => new Project
    {
        Name = dto.Name,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    public static void UpdateEntity(Project existing, CreateProjectDto dto)
    {
        existing.Name = dto.Name;
        existing.UpdatedAt = DateTime.UtcNow;
    }
}