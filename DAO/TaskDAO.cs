using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using owi_back.Context;
using owi_back.DTO;
using owi_back.Models;
//using owi_back.Mapping;

namespace owi_back.DAO;

public class TaskDAO
{
    private readonly OwidbContext _context;

    public TaskDAO(OwidbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<owi_back.Models.Task>> GetTasks()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<owi_back.Models.Task> GetTask(int id)
    {
        // return await _context.Tasks.FindAsync(id);
        return await _context.Tasks.Include(t => t.Comments).FirstOrDefaultAsync(t => t.Id == id);
    }

    //-- avec DTO
    /* public async Task<owi_back.Models.Task> GetTaskDTO(int id)
     {
         return await _context.Tasks.FindAsync(id);
     }*/


    public async Task<owi_back.Models.Task> AddTask(owi_back.Models.Task task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        /*if (task.Id != 0)
        {
            var taskToAdd = await _context
                .Tasks.Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == Task.TaskId.Value);

            taskToAdd.Tasks.Add(Task);
        }
        await _context.SaveChangesAsync();*/

        return task;
    }

    public async Task<owi_back.Models.Task> UpdateTask(owi_back.Models.Task task)
    {
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}
