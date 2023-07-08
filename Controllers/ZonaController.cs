using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEsp32Master.Context;
using WebApiEsp32Master.Domain;

namespace WebApiEsp32Master.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ZonaController : ControllerBase
{
    private readonly ZonaDbContext dbContext;

    public ZonaController(ZonaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Zona>>> GetZonas()
    {
        var zonas = await dbContext.Zonas.ToListAsync();
        return Ok(zonas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Zona>> GetZona(int id)
    {
        var zona = await dbContext.Zonas.FindAsync(id);

        if (zona == null)
        {
            return NotFound();
        }

        return Ok(zona);
    }

    [HttpPost]
    public async Task<ActionResult<Zona>> CreateZona(Zona zona)
    {
        dbContext.Zonas.Add(zona);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateZona), new { id = zona.Id}, zona);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateZona(int id, Zona zona)
    {
        if (id != zona.Id)
        {
            return BadRequest();
        }

        dbContext.Entry(zona).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (!ZonaExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteZona(int id)
    {
        var zona = await dbContext.Zonas.FindAsync(id);

        if (zona == null)
        {
            return NotFound();
        }

        dbContext.Zonas.Remove(zona);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool ZonaExists(int id)
    {
        return dbContext.Zonas.Any(z => z.Id == id);
    }
}
