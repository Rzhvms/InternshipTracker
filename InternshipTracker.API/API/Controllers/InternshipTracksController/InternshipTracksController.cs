using InternshipTracker.API.Controllers.DTO;
using InternshipTracker.API.Data;
using InternshipTracker.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternshipTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InternshipTracksController : ControllerBase
{
    private readonly AppDbContext _context;

    public InternshipTracksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InternshipTrackDto>>> GetInternshipTracks()
    {
        var tracks = await _context.InternshipTracks
            .Select(t => InternshipTrackMapper.ToDto(t))
            .ToListAsync();

        return Ok(tracks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InternshipTrackDto>> GetInternshipTrack(int id)
    {
        var track = await _context.InternshipTracks.FindAsync(id);
        if (track == null) return NotFound();

        return Ok(InternshipTrackMapper.ToDto(track));
    }

    [HttpPost]
    public async Task<ActionResult<InternshipTrackDto>> CreateInternshipTrack([FromBody] CreateInternshipTrackDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var track = InternshipTrackMapper.ToEntity(dto);

        _context.InternshipTracks.Add(track);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInternshipTrack), new { id = track.Id }, InternshipTrackMapper.ToDto(track));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInternshipTrack(int id, [FromBody] CreateInternshipTrackDto dto)
    {
        var track = await _context.InternshipTracks.FindAsync(id);
        if (track == null) return NotFound();

        InternshipTrackMapper.UpdateEntity(track, dto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInternshipTrack(int id)
    {
        var track = await _context.InternshipTracks
            .Include(t => t.Interns)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (track == null) return NotFound();

        if (track.Interns.Any())
            return BadRequest("Нельзя удалить направление, так как с ним связаны стажёры");

        _context.InternshipTracks.Remove(track);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
