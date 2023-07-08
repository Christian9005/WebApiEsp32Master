using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEsp32Master.Context;
using WebApiEsp32Master.Domain;
using WebApiEsp32Master.Domain.Dtos;

namespace WebApiEsp32Master.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly ZonaDbContext dbContext;

    private static int lastCreatedTaskId;

    public TasksController(ZonaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await dbContext.Tasks
            .Include(t => t.Person)
            .Include(t => t.Tag)
            .Select(t => new {
                t.Id,
                t.Description,
                t.StartTime,
                t.EndTime,
                t.PersonId,
                Person = t.Person, // Incluye la información de la persona
                GroupId = t.GroupId,
                Group = dbContext.Groups.FirstOrDefault(g => g.Id == t.GroupId), // Obtén el grupo por su ID
                t.TagId,
                t.Tag
            })
            .ToListAsync();

        return Ok(tasks);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Tasks>> GetTask(int id)
    {
        var task = await dbContext.Tasks.Include(t => t.Person).Include(t => t.Group).Include(t => t.Tag).FirstOrDefaultAsync(t => t.Id == id);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskDto taskDto)
    {
        if (taskDto == null)
        {
            return BadRequest("Task data is null");
        }

        if (ModelState.IsValid)
        {
            if ((taskDto.PersonId == null && taskDto.GroupId == null) || (taskDto.PersonId != null && taskDto.GroupId != null))
            {
                ModelState.AddModelError("", "Either PersonId or GroupId should be provided.");
                return BadRequest(ModelState);
            }

            var task = new Tasks
            {
                Description = taskDto.Description,
                StartTime = taskDto.StartTime,
                // No asignar el TagId en este punto
            };

            if (taskDto.PersonId != null)
            {
                var existingPerson = await dbContext.People.FindAsync(taskDto.PersonId);
                if (existingPerson == null)
                {
                    ModelState.AddModelError("PersonId", "Invalid PersonId.");
                    return BadRequest(ModelState);
                }

                task.PersonId = taskDto.PersonId;
            }

            if (taskDto.GroupId != null)
            {
                var existingGroup = await dbContext.Groups.FindAsync(taskDto.GroupId);
                if (existingGroup == null)
                {
                    ModelState.AddModelError("GroupId", "Invalid GroupId.");
                    return BadRequest(ModelState);
                }

                task.GroupId = taskDto.GroupId;
            }

            dbContext.Tasks.Add(task);
            await dbContext.SaveChangesAsync();

            lastCreatedTaskId = task.Id;

            return CreatedAtAction(nameof(CreateTask), new { id = task.Id }, task);
        }

        return BadRequest(ModelState);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await dbContext.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("complete/{tagId}")]
    public async Task<IActionResult> CompleteTaskByTagId(int tagId, int taskID)
    {
        var task = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskID && t.TagId == null);

        if (task == null)
        {
            return NotFound();
        }

        task.TagId = tagId;
        task.EndTime = DateTime.Now.AddHours(-5);
        await dbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("lastCreatedTaskId")]
    public IActionResult GetLastCreatedTaskId()
    {
        return Ok(lastCreatedTaskId);
    }

    [HttpGet("eliminarTareas")]
    public IActionResult EliminarTareas()
    {
        var today = DateTime.Today;
        var tasksToDelete = dbContext.Tasks.Where(t => t.EndTime < today).ToList();

        dbContext.Tasks.RemoveRange(tasksToDelete);
        dbContext.SaveChanges();

        return Ok();
    }

}
