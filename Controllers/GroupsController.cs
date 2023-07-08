using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEsp32Master.Context;
using WebApiEsp32Master.Domain;
using WebApiEsp32Master.Domain.Dtos;
namespace WebApiEsp32Master.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupsController : ControllerBase
{
    private readonly ZonaDbContext dbContext;

    public GroupsController(ZonaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
    {
        var groups = await dbContext.Groups.Include(g => g.People).ToListAsync();

        return Ok(groups);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> GetGroup(int id)
    {
        var group = await dbContext.Groups.Include(g => g.People).FirstOrDefaultAsync(g => g.Id == id);

        if (group == null)
        {
            return NotFound();
        }

        return Ok(group);
    }

    [HttpPost]
    public async Task<ActionResult<Group>> CreateGroup(CreateGroupDto createGroupDto)
    {
        var people = await dbContext.People.Where(p => createGroupDto.PeopleIds.Contains(p.Id)).ToListAsync();

        var group = new Group
        {
            Name = createGroupDto.Name,
            People = people
        };

        dbContext.Groups.Add(group);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGroup), new { id = group.Id }, group);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroup(int id, Group updatedGroup)
    {
        if (id != updatedGroup.Id)
        {
            return BadRequest();
        }

        var group = await dbContext.Groups.Include(g => g.People).FirstOrDefaultAsync(g => g.Id == id);

        if (group == null)
        {
            return NotFound();
        }

        group.Name = updatedGroup.Name;
        group.People = updatedGroup.People;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (!GroupExists(id))
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
    public async Task<IActionResult> DeleteGroup(int id)
    {
        var group = await dbContext.Groups.FindAsync(id);

        if (group == null)
        {
            return NotFound();
        }

        var people = dbContext.People.Where(p => p.GroupId == id);
        foreach (var person in people)
        {
            person.GroupId = null;
        }

        dbContext.Groups.Remove(group);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool GroupExists(int id)
    {
        return dbContext.Groups.Any(g => g.Id == id);
    }
}
