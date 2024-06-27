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

        public async System.Threading.Tasks.Task<IEnumerable<Listing>> GetAllAsync()
        {
            return await _context.Listings.ToListAsync();
        }

        public async System.Threading.Tasks.Task<Listing> GetByIdAsync(int id)
        {
            return await _context.Listings.FindAsync(id);
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