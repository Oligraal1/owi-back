using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByTask(int taskId)
    {
        var response = await _DAO.GetComments(taskId);
        return Ok(response.Select(t =>_mapper.CommentToDTO(t)));
    }

    [HttpGet("id/{taskId}")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentById(int id,int taskId)
    {
        var comments = await _DAO.GetComment(id,taskId);
        if (comments == null)
        {
            return NotFound();
        }
        return Ok(_mapper.CommentToDTO(comments));
    }



    // PUT: api/Comment/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComment(int id, CommentDTO commentDTO)
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
    }

        [HttpPost]
         public async Task<ActionResult<CommentDTO>> CreateComment(CommentDTO commentDTO)
        {
            var comment = new Comment { 
                Content = commentDTO.Content,
                TaskId = commentDTO.TaskId
             };

            var createdComment = await _DAO.AddComment(comment);
            return CreatedAtAction(
                nameof(GetCommentById),
                new {
                     id = createdComment.Id,
                     taskId = createdComment.TaskId
                     },
                _mapper.CommentToDTO(createdComment)
            );
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID incorrect", nameof(id));
            }

            var response = await _DAO.DeleteCommentById(id);
            return Ok(response);
        }

    }
