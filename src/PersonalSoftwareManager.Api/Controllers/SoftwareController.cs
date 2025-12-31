using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalSoftwareManager.Contracts.Models;
using PersonalSoftwareManager.Data.Context;

namespace PersonalSoftwareManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SoftwareController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<SoftwareController> _logger;

    public SoftwareController(ApplicationDbContext context, ILogger<SoftwareController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Software>>> GetAll()
    {
        return await _context.Software
            .Include(s => s.Comments)
            .Include(s => s.Screenshots)
            .Include(s => s.Urls)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Software>> GetById(int id)
    {
        var software = await _context.Software
            .Include(s => s.Comments)
            .Include(s => s.Screenshots)
            .Include(s => s.Urls)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (software == null)
        {
            return NotFound();
        }

        return software;
    }

    [HttpPost]
    public async Task<ActionResult<Software>> Create(Software software)
    {
        software.CreatedAt = DateTime.UtcNow;
        _context.Software.Add(software);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = software.Id }, software);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Software software)
    {
        if (id != software.Id)
        {
            return BadRequest();
        }

        software.UpdatedAt = DateTime.UtcNow;
        _context.Entry(software).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await SoftwareExists(id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var software = await _context.Software.FindAsync(id);
        if (software == null)
        {
            return NotFound();
        }

        _context.Software.Remove(software);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> SoftwareExists(int id)
    {
        return await _context.Software.AnyAsync(e => e.Id == id);
    }
}
