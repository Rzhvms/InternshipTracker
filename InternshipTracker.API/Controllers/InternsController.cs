using InternshipTracker.API.Data;
using InternshipTracker.API.Entities;
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
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetIntern(Guid id)
    {
        var intern = await _context.Interns
            .Include(i => i.Track)
            .Include(i => i.Project)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (intern == null)
        {
            return NotFound();
        }
        return Ok(intern);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIntern([FromBody] Intern intern)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var emailExists = await _context.Interns.AnyAsync(i => i.Email == intern.Email);
        if (emailExists)
        {
            return BadRequest("Email уже используется.");
        }

        if (!string.IsNullOrWhiteSpace(intern.Phone))
        {
            var phoneExists = await _context.Interns.AnyAsync(i => i.Phone == intern.Phone);
            if (phoneExists)
            {
                return BadRequest("Телефон уже используется.");
            }
        }
        
        _context.Interns.Add(intern);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetIntern), new { id = intern.Id }, intern);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateIntern(Guid id, [FromBody] Intern intern)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingIntern = await _context.Interns.FindAsync(id);
        if (existingIntern == null)
        {
            return NotFound();
        }
        
        if (existingIntern.Email != intern.Email && 
            await _context.Interns.AnyAsync(i => i.Email == intern.Email))
        {
            return BadRequest("Email уже используется");
        }

        if (!string.IsNullOrWhiteSpace(intern.Phone) && 
            existingIntern.Phone != intern.Phone &&
            await _context.Interns.AnyAsync(i => i.Phone == intern.Phone))
        {
            return BadRequest("Телефон уже используется");
        }

        _context.Entry(existingIntern).CurrentValues.SetValues(intern);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteIntern(Guid id)
    {
        var intern = await _context.Interns.FindAsync(id);

        if (intern == null)
        {
            return NotFound();
        }

        _context.Interns.Remove(intern);
        await _context.SaveChangesAsync();

        return Ok();
    }
}