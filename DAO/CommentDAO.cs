using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using owi_back.Context;
using owi_back.Models;

//using owi_back.Mapping;

namespace owi_back.DAO;

public class CommentDAO
{
    private readonly OwidbContext _context;

    public CommentDAO(OwidbContext context)
    {
        _context = context;
    }
///
    public async Task<IEnumerable<Comment>> GetComments(int taskId)
    {
        return await _context.Comments
                .Where(comment =>comment.TaskId == taskId)
                .ToListAsync();
    }

    public async Task<Comment> GetComment(int id, int taskId)
    {
        return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id && c.TaskId == taskId);
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return comment;
    }
    public async Task<Comment> UpdateComment(Comment comment)
    {
         _context.Entry(comment).State = EntityState.Modified;
         await _context.SaveChangesAsync();
         return comment;
    }

    public async Task<bool> DeleteCommentById(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return false;
        }
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return true;
    }

}
