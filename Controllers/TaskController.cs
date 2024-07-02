using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using owi_back.Context;
using owi_back.DAO;
using owi_back.DTO;
using owi_back.Mapping;
using owi_back.Models;

namespace owi_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskDAO _DAO;
    private readonly Mapper _mapper;

    public TaskController(TaskDAO dao, Mapper mapper)
    {
        _DAO = dao;
        _mapper = mapper;
    }

    // GET: api/Task
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<owi_back.Models.Task>>> GetTasks()
    {
        var response = await _DAO.GetTasks();
        return Ok(response);
    }

    // GET: api/Task/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDTO>> GetTask(int id)
    {
        /*var response = await _DAO.GetTask(id);
        return Ok(response);*/
        var task = await _DAO.GetTask(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    // GET: api/Task/Listing/5
        [HttpGet("Listing/{listingId}")]
        public async Task<ActionResult<IEnumerable<owi_back.Models.Task>>> GetTasksByListingId(int listingId)
        {
            var tasks = await _DAO.GetTasksByListingId(listingId);
            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

    // PUT: api/Task/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTask(int id, owi_back.Models.Task task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        var result = await _DAO.UpdateTask(task);
        return Ok(result);
    }

    // POST: api/Task
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost("")]
    public async Task<ActionResult<owi_back.Models.Task>> PostTask(owi_back.Models.Task task)
    {
        var response = await _DAO.AddTask(task);
        return Ok(response);
    }

    // DELETE: api/Task/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var response = await _DAO.DeleteTask(id);
        return Ok(response);
    }
}
