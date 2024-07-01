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
        /*var response = await _DAO.GetTasks();
        return Ok(response);*/
        var tasks = await _DAO.GetTasks();
        return Ok(tasks.Select(t => _mapper.TaskToDTO(t)));
    }

    // GET: api/Task/5
    [HttpGet("{id}/{projectId}")]
    public async Task<ActionResult<TaskDTO>> GetTaskById(int id, int listingId)
    {
        /*var response = await _DAO.GetTask(id);
        return Ok(response);*/
        var task = await _DAO.GetTask(id, listingId);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(_mapper.TaskToDTO(task));
    }

    /*[HttpGet("{id}/{projectId}")]
        public async Task<ActionResult<Listing>> GetListingByIdAndProjectId(int id, int projectId)
        {
            var listing = await _listingDao.GetByIdAndProjectIdAsync(id, projectId);

            if (listing == null)
            {
                return NotFound();
            }

            return Ok(listing);
        }*/


    // PUT: api/Task/5

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, TaskDTO taskDTO)
    {
        if (id != taskDTO.Id)
        {
            return BadRequest();
        }

        var task = new owi_back.Models.Task
        {
            Id = taskDTO.Id,
            Name = taskDTO.Name,
            Tag = taskDTO.Tag,
            Comments = taskDTO
                .Comments?.Select(c => new Comment
                {
                    Id = c.Id,
                    Content = c.Content,
                    TaskId = c.TaskId
                })
                .ToList()
        };

        try
        {
            await _DAO.UpdateTask(task);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _DAO.GetTask(id, (int)task.ListingId) == null)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /*[HttpPut("{id}")]
    public async Task<IActionResult> PutTask(int id, owi_back.Models.Task task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        var result = await _DAO.UpdateTask(task);
        return Ok(result);
    }*/

    // POST: api/Task
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

    /*public async Task<ActionResult<owi_back.Models.Task>> PostTask(owi_back.Models.Task task)
    {
        var response = await _DAO.AddTask(task);
        return Ok(response);
    }*/
    [HttpPost]
    public async Task<ActionResult<TaskDTO>> PostTask(TaskDTO taskDTO)
    {
        var task = new owi_back.Models.Task
        {
            Name = taskDTO.Name,
            Tag = taskDTO.Tag,
            Comments = taskDTO
                .Comments?.Select(c => new Comment { Content = c.Content, TaskId = c.TaskId })
                .ToList()
        };


        var createdTask = await _DAO.AddTask(task);
        return CreatedAtAction(
            nameof(GetTaskById),
            new { id = createdTask.Id },
            _mapper.TaskToDTO(createdTask)
        );
    }

    // DELETE: api/Task/5
   /* [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID incorrect", nameof(id));
        }

        var response = await _DAO.DeleteTask(id);
        return Ok(response);
    }*/

 [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id, int projectId)
        {
            var listing = await _DAO.GetTask(id, projectId);
            if (listing == null)
            {
                return NotFound();
            }

            await _DAO.DeleteAsync(id);
            return NoContent();
        }





}
