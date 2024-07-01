using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using owi_back.DAO_DAO;
using owi_back.DAO;
using owi_back.DTO;
using owi_back.Mapping;
using owi_back.Models;

namespace owi_back.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly CommentDAO _DAO;
    private readonly Mapper _mapper;

    public CommentController(CommentDAO dao, Mapper mapper)
    {
        _DAO = dao;
        _mapper = mapper;
    }

    // GET: api/Comment
    /*[HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
    {
        var response = await _DAO.GetComments();
        return Ok(response);*/

    [HttpGet("task/{taskId}")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments(int taskId)
    {
        var comments = await _DAO.GetComments(taskId);
        return Ok(comments.Select(c => _mapper.CommentToDTO(c)));
    }

    // GET: api/Comment/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDTO>> GetComment(int id, int taskId)
    {
        var comment = await _DAO.GetComment(id, taskId);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(_mapper.CommentToDTO(comment));
    }

    // PUT: api/Comment/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> CreatComment(int id, CommentDTO commentDTO)
    {
        if (id != commentDTO.Id)
        {
            return BadRequest();
        }

        var comment = new Comment
        {
            Id = commentDTO.Id,
            Content = commentDTO.Content,
            TaskId = commentDTO.TaskId
        };

        try
        {
            await _DAO.UpdateComment(comment);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _DAO.GetComment(id, (int)comment.TaskId) == null)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();

        // POST: api/Comment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment Comment)
        {
            var response = await _DAO.AddComment(Comment);
            return Ok(response);
        }*/

        [HttpPost]
         public async Task<ActionResult<CommentDTO>> CreateComment(CommentDTO commentDTO)
        {
            var comment = new Comment { Content = commentDTO.Content, TaskId = commentDTO.TaskId };

            var createdComment = await _DAO.AddComment(comment);
            return CreatedAtAction(
                nameof(GetComment),
                new { id = createdComment.Id, taskId = createdComment.TaskId },
                _mapper.CommentToDTO(createdComment)
            );
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var deleted = await _DAO.DeleteComment(id);
                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if necessary
                return StatusCode(500, "Internal server error");
            }        
            
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id, int projectId)
        {
            var listing = await _listingDao.GetByIdAndProjectIdAsync(id, projectId);
            if (listing == null)
            {
                return NotFound();
            }

            await _listingDao.DeleteAsync(id);
            return NoContent();
        }


        /*[HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID incorrect", nameof(id));
        }

        var response = await _DAO.DeleteTask(id);
        return Ok(response);
    }*/

    }
}
