using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEsp32Master.Context;
using WebApiEsp32Master.Domain;

namespace WebApiEsp32Master.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ZonaDbContext dbContext;

    public AdminController(ZonaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Admin>>> GetAdmins()
    {
        var admins = await dbContext.Admins.ToListAsync();
        return Ok(admins);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Admin>> GetAdmin(int id)
    {
        var admin = await dbContext.Admins.FindAsync(id);

        if (admin == null)
        {
            return NotFound();
        }

        return Ok(admin);
    }

    [HttpPost]
    public async Task<ActionResult<Admin>> CreateAdmin(Admin admin)
    {
        dbContext.Admins.Add(admin);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateAdmin),new { id = admin.Id}, admin);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAdmin(int id, Admin updatedAdmin)
    {
        if (id != updatedAdmin.Id)
        {
            return BadRequest();
        }

        dbContext.Entry(updatedAdmin).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (!AdminExists(id))
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
    public async Task<IActionResult> DeleteAdmin(int id)
    {
        var admin = await dbContext.Admins.FindAsync(id);

        if (admin == null)
        {
            return NotFound();
        }

        dbContext.Admins.Remove(admin);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool AdminExists(int id)
    {
        return dbContext.Admins.Any(a => a.Id == id);
    }

}
