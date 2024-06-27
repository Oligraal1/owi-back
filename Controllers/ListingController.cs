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

        // GET: api/Listing
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Listing>>> GetListings()
        {
            var listings = await _listingDao.GetAllAsync();
            return Ok(listings);
        }

        // GET: api/Listing/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(int id)
        {
            var listing = await _listingDao.GetByIdAsync(id);

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
            await _listingDao.AddAsync(listing);
            return CreatedAtAction(nameof(GetListing), new { id = listing.Id }, listing);
        }

        // PUT: api/Listing/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(int id, Listing listing)
        {
            if (id != listing.Id)
            {
                return BadRequest();
            }

            try
            {
                await _listingDao.UpdateAsync(listing);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _listingDao.GetByIdAsync(id) == null)
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

        // DELETE: api/Listing/5
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
