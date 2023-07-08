using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEsp32Master.Context;
using WebApiEsp32Master.Domain;

namespace WebApiEsp32Master.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PeopleController : ControllerBase
{
    private readonly ZonaDbContext dbContext;

    public PeopleController(ZonaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
    {
        var people = await dbContext.People.ToListAsync();
        return Ok(people);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Person>> GetPerson(int id)
    {
        var person = await dbContext.People.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    [HttpPost]
    public async Task<ActionResult<Person>> CreatePerson(Person person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (person.GroupId == 0)
        {
            person.GroupId = null;
        }

        dbContext.People.Add(person);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, Person updatePerson) 
    {
        if (id != updatePerson.Id)
        {
            return BadRequest();
        }

        dbContext.Entry(updatePerson).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (!PersonExists(id))
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
    public async Task<IActionResult> DeletePerson(int id)
    {
        var person = await dbContext.People.FindAsync(id);

        if (person == null)
        {
            return NotFound();
        }

        dbContext.People.Remove(person);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    private bool PersonExists(int id)
    {
        return dbContext.People.Any(p => p.Id == id);
    }
}
