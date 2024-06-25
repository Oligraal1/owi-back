// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;
// using owi_back.Models;

// namespace owi_back.DAO;


// public class CommentDAO
// {
//     private readonly Context _context;

//     public CommentDAO(Context context)
//     {
//         _context = context;
//     }

//     public async Task<IEnumerable<CommentDAO>> GetCommentDAOs()
//     {
//         return await _context.CommentDAOs.ToListAsync();
//     }

//     public async Task<CommentDAO> GetCommentDAO(int id)
//     {
//         return await _context.CommentDAOs.FindAsync(id);
//     }

//     public async Task<CommentDAO> AddCommentDAO(CommentDAO CommentDAO)
//     {
//         _context.CommentDAOs.Add(CommentDAO);
//         await _context.SaveChangesAsync();

//         if (CommentDAO.taskId != 0)
//         {
//             var task = await _context
//                 .tasks.Include(p => p.CommentDAOs)
//                 .FirstOrDefaultAsync(p => p.taskId == CommentDAO.taskId.Value);

//             task.CommentDAOs.Add(CommentDAO);
//         }
//         await _context.SaveChangesAsync();

//         return CommentDAO;
//     }

//     public async Task<CommentDAO> UpdateCommentDAO(CommentDAO CommentDAO)
//     {
//         _context.Entry(CommentDAO).State = EntityState.Modified;
//         await _context.SaveChangesAsync();
//         return CommentDAO;
//     }

//     public async Task<bool> DeleteCommentDAO(int id)
//     {
//         var CommentDAO = await _context.CommentDAOs.FindAsync(id);
//         if (CommentDAO == null)
//             return false;

//         _context.CommentDAOs.Remove(CommentDAO);
//         await _context.SaveChangesAsync();
//         return true;
//     }
// }
