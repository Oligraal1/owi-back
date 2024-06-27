using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using owi_back.Models;
using owi_back.Context;

namespace owi_back.DAO
{
    public class ProjectDao
    {
        private readonly OwidbContext _context;

        public ProjectDao(OwidbContext context)
        {
            _context = context;
        }
    public async System.Threading.Tasks.Task<IEnumerable<Project>> GetAllProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async System.Threading.Tasks.Task<Project?> GetProjectById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID incorrect", nameof(id));
            }
            return await _context.Projects.FindAsync(id);
        }

        public async System.Threading.Tasks.Task AddProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (string.IsNullOrEmpty(project.Name))
            {
                throw new ArgumentException("Le nom du projet doit être spécifié", nameof(project.Name));
            }

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateProject(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (project.Id <= 0)
            {
                throw new ArgumentException("ID incorrect", nameof(project.Id));
            }

            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteProjectById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID incorrect", nameof(id));
            }

            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Projet non trouvé");
            }
        }
    }
}