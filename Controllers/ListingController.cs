using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using owi_back.Models;
using owi_back.DAO;

namespace owi_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController : ControllerBase
    {
        private readonly ListingDao _listingDao;

        public ListingController(ListingDao listingDao)
        {
            _listingDao = listingDao;
        }

        // GET: api/Listing/1
        [HttpGet("{projectId}")]
        public async Task<ActionResult<IEnumerable<Listing>>> GetListingsByProjectId(int projectId)
        {
            var listings = await _listingDao.GetAllByProjectIdAsync(projectId);
            return Ok(listings);
        }

         // GET: api/Listing/5/1
        [HttpGet("{id}/{projectId}")]
        public async Task<ActionResult<Listing>> GetListingByIdAndProjectId(int id, int projectId)
        {
            var listing = await _listingDao.GetByIdAndProjectIdAsync(id, projectId);

            if (listing == null)
            {
                return NotFound();
            }

            return Ok(listing);
        }

        // POST: api/Listing
        [HttpPost("")]
        public async Task<ActionResult<Listing>> PostListing(Listing listing)
        {
            try
            {
                // _context.Listings.Add(listing);
                // await _context.SaveChangesAsync();
                await _listingDao.AddAsync(listing);
                return CreatedAtAction(nameof(GetListingByIdAndProjectId), new { id = listing.Id, projectId = listing.ProjectId }, listing);
            }
            catch (Exception ex)
            {
                // Log exception details
                // This can be replaced with your logging framework of choice
                Console.WriteLine($"Error adding listing: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw; // Re-throw the exception to ensure it's not swallowed
            }
            
        }

        // PUT: api/Listing/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(int id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }
               await _listingDao.UpdateAsync(listing);
               return NoContent();
            
        }

        // DELETE: api/Listing/5/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(int id)
        {
            var listing = await _listingDao.GetByIdAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            await _listingDao.DeleteAsync(id);
            return NoContent();
        }
    }
}
