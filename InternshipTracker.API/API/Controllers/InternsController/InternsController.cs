using InternshipTracker.API.Controllers.DTO;
using InternshipTracker.API.Data;
using InternshipTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InternsController : ControllerBase
{
    private readonly AppDbContext _context;

    public InternsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetIntern(int id)
    {
        var intern = await _context.Interns
            .Include(i => i.Track)
            .Include(i => i.Project)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (intern == null)
            return NotFound();

        return Ok(InternMapper.ToDto(intern));
    }

    [HttpPost]
    public async Task<IActionResult> CreateIntern([FromBody] CreateInternDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var validationError = await ValidateIntern(dto);
        if (validationError != null)
            return BadRequest(validationError);

        var intern = InternMapper.ToEntity(dto);

        _context.Interns.Add(intern);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetIntern), new { id = intern.Id }, InternMapper.ToDto(intern));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateIntern(int id, [FromBody] CreateInternDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var intern = await _context.Interns.FindAsync(id);
        if (intern == null)
            return NotFound();

        var validationError = await ValidateIntern(dto, intern);
        if (validationError != null)
            return BadRequest(validationError);

        InternMapper.UpdateEntity(intern, dto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteIntern(int id)
    {
        var intern = await _context.Interns.FindAsync(id);
        if (intern == null)
            return NotFound();

        _context.Interns.Remove(intern);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<string?> ValidateIntern(CreateInternDto dto, Intern? existingIntern = null)
    {
        if (!await _context.InternshipTracks.AnyAsync(t => t.Id == dto.TrackId))
            return "Invalid TrackId";

        if (dto.ProjectId.HasValue && !await _context.Projects.AnyAsync(p => p.Id == dto.ProjectId))
            return "Invalid ProjectId";

        if (existingIntern == null || existingIntern.Email != dto.Email)
        {
            if (await _context.Interns.AnyAsync(i => i.Email == dto.Email))
                return "Email уже используется";
        }

        if (!string.IsNullOrWhiteSpace(dto.PhoneNumber) &&
            (existingIntern == null || existingIntern.PhoneNumber != dto.PhoneNumber))
        {
            if (await _context.Interns.AnyAsync(i => i.PhoneNumber == dto.PhoneNumber))
                return "Телефон уже используется";
        }

        return null;
    }
}
