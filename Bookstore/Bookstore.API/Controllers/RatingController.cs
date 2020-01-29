using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.API.Data;
using Bookstore.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    public class RatingController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public RatingController(ApplicationDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Ratings = await _context.Ratings.ToListAsync();
            if (Ratings != null)
            {
                return Ok(Ratings);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("GetUserRatings/{id}")]
        public async Task<IActionResult> GetUserRatings(string id)
        {
            var Ratings = await _context.Ratings.Where(x => x.AppUserFK_SysID.Equals(id)).ToListAsync();
            if (Ratings != null)
            {
                return Ok(Ratings);
            }
            return BadRequest();
        }

        [HttpGet("GetBookRatings/{id}")]
        public async Task<IActionResult> GetBookRatings(int id)
        {
            var Ratings = await _context.Ratings.Where(x => x.BookFK_SysID.Equals(id)).ToListAsync();
            if (Ratings != null)
            {
                return Ok(Ratings);
            }
            return BadRequest();
        }

        [HttpPost("GetRatingUserBook")]
        public async Task<IActionResult> GetRatingUserBook([FromBody]Rating value)
        {
            var Ratings = await _context.Ratings.FirstOrDefaultAsync(x => x.AppUserFK_SysID.Equals(value.AppUserFK_SysID) && x.BookFK_SysID == value.BookFK_SysID);
            if (Ratings != null)
            {
                return Ok(Ratings);
            }
            return BadRequest();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Rating value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Ratings.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.RatingSysID }, value);
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody]Rating value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Rating = await _context.Ratings.FirstOrDefaultAsync(x => x.RatingSysID == value.RatingSysID);
            if (Rating == null)
            {
                return NotFound();
            }
            Rating.RatingGrade = value.RatingGrade;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(UpdateItem), new { id = Rating.RatingSysID }, Rating);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var Rating = await _context.Ratings.FirstOrDefaultAsync(x => x.RatingSysID == id);
            if (Rating == null)
            {
                return NotFound();
            }
            _context.Ratings.Remove(Rating);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
