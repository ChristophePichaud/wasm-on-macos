using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalSoftwareManager.Contracts.Models;
using PersonalSoftwareManager.Data.Context;

namespace PersonalSoftwareManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OpenSourceProjectsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<OpenSourceProjectsController> _logger;

    public OpenSourceProjectsController(ApplicationDbContext context, ILogger<OpenSourceProjectsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OpenSourceProject>>> GetAll()
    {
        return await _context.OpenSourceProjects
            .Include(p => p.Comments)
            .Include(p => p.Screenshots)
            .Include(p => p.Urls)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OpenSourceProject>> GetById(int id)
    {
        var project = await _context.OpenSourceProjects
            .Include(p => p.Comments)
            .Include(p => p.Screenshots)
            .Include(p => p.Urls)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
        {
            return NotFound();
        }

        return project;
    }

    [HttpPost]
    public async Task<ActionResult<OpenSourceProject>> Create(OpenSourceProject project)
    {
        project.CreatedAt = DateTime.UtcNow;
        _context.OpenSourceProjects.Add(project);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OpenSourceProject project)
    {
        if (id != project.Id)
        {
            return BadRequest();
        }

        project.UpdatedAt = DateTime.UtcNow;
        _context.Entry(project).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ProjectExists(id))
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
        var project = await _context.OpenSourceProjects.FindAsync(id);
        if (project == null)
        {
            return NotFound();
        }

        _context.OpenSourceProjects.Remove(project);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> ProjectExists(int id)
    {
        return await _context.OpenSourceProjects.AnyAsync(e => e.Id == id);
    }
}
