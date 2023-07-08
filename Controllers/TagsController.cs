using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEsp32Master.Context;
using WebApiEsp32Master.Domain;

namespace WebApiEsp32Master.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ZonaDbContext dbContext;

    public TagsController(ZonaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
    {
        var tags = await dbContext.Tags.ToListAsync();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tag>> GetTag(int id)
    {
        var tag = await dbContext.Tags.FindAsync(id);

        if (tag == null) 
        {
            return NotFound();
        }

        return Ok(tag);
    }

    [HttpGet("uid/{uid}")]
    public async Task<ActionResult<int>> GetTagIdByUid(string uid)
    {
        var tag = await dbContext.Tags.FirstOrDefaultAsync(t => t.CodeNumber == uid);

        if (tag == null)
        {
            return NotFound();
        }

        return Ok(tag.Id);
    }

    [HttpPost]
    public async Task<ActionResult<Tag>> CreateTag(Tag tag)
    {
        dbContext.Tags.Add(tag);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateTag), new { id = tag.Id }, tag);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(int id, Tag tag)
    {
        if (id != tag.Id)
        {
            return BadRequest();
        }

        dbContext.Entry(tag).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (!TagExists(id))
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
    public async Task<IActionResult> DeleteTag(int id)
    {
        var tag = await dbContext.Tags.FindAsync(id);

        if (tag == null) 
        { 
            return NotFound();
        }

        dbContext.Tags.Remove(tag);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool TagExists(int id)
    {
        return dbContext.Tags.Any(t => t.Id == id);
    }
}
