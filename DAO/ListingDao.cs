using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using owi_back.Models;
using owi_back.Context;

namespace owi_back.DAO
{
    public class ListingDao
    {
        private readonly OwidbContext _context;

        public ListingDao(OwidbContext context)
        {
            _context = context;
        }

        // Récupérer tous les listings pour un projet spécifique
        public async Task<IEnumerable<Listing>> GetAllByProjectIdAsync(int projectId)
        {
            return await _context.Listings
                                 .Where(listing => listing.ProjectId == projectId)
                                 .ToListAsync();
        }

        // Récupérer un listing par son Id et par le projet auquel il appartient
        public async Task<Listing> GetByIdAndProjectIdAsync(int id, int projectId)
        {
            return await _context.Listings.FirstOrDefaultAsync(listing => listing.Id == id && listing.ProjectId == projectId);
        }

        public async System.Threading.Tasks.Task AddAsync(Listing listing)
        {
            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task UpdateAsync(Listing listing)
        {
            _context.Entry(listing).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing != null)
            {
                _context.Listings.Remove(listing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
