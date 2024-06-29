using System.Diagnostics;
using System.Linq;
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
        //return await _context.Tasks.FindAsync(id);
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<owi_back.Models.Task>> GetTasksByListingId(int listingId)
        {
            return await _context.Tasks
                .Where(t => t.ListingId == listingId)
                .ToListAsync();
        }

    public async Task<owi_back.Models.Task> AddTask(owi_back.Models.Task task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

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
