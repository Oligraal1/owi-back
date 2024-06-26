using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using owi_back.Models;
using owi_back.Context;
using owi_back.DTO;
//using owi_back.Mapping;

namespace owi_back.DAO;


public class CommentDAO
{
    private readonly OwidbContext _context;

    public CommentDAO(OwidbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Comment>> GetComments()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment> GetComment(int id)
    {
        return await _context.Comments.FindAsync(id);
    }
//-- avec DTO
    public async Task<Comment> GetCommentDTO(int id)
    {
        return await _context.Comments.FindAsync(id);
    }


    public async Task<Comment> AddComment(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        if (comment.Id != 0)
        {
            var task = await _context
                .Tasks.Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == comment.TaskId.Value);

            task.Comments.Add(comment);
        }
        await _context.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment> UpdateComment(Comment comment)
    {
        _context.Entry(comment).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<bool> DeleteComment(int id)
    {
        var Comment = await _context.Comments.FindAsync(id);
        if (Comment == null)
            return false;

        _context.Comments.Remove(Comment);
        await _context.SaveChangesAsync();
        return true;
    }
}
