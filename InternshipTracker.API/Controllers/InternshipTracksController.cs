using InternshipTracker.API.Data;
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
    public async Task<IActionResult> GetInternshipTracks()
    {
        var tracks = await _context.InternshipTracks.ToListAsync();
        return Ok(tracks);
    }
}