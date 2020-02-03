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
    public class FavoriteController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public FavoriteController(ApplicationDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Favorites = await _context.Favorites.Include(x => x.Book).ThenInclude(x => x.Author).ToListAsync();
            if (Favorites != null)
            {
                return Ok(Favorites);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("GetUserFavorites")]
        public async Task<IActionResult> GetUserBookFavorites([FromBody]Favorite value)
        {
            var Favorites = await _context.Favorites.Where(x => x.AppUserFK_SysID.Equals(value.AppUserFK_SysID)).ToListAsync();
            if (Favorites != null)
            {
                return Ok(Favorites);
            }
            return BadRequest();
        }
        
        [HttpPost("GetFavoriteUserBook")]
        public async Task<IActionResult> GetFavoriteUserBook([FromBody]Favorite value)
        {
            var Favorites = await _context.Favorites.FirstOrDefaultAsync(x => x.AppUserFK_SysID.Equals(value.AppUserFK_SysID) && x.BookFK_SysID == value.BookFK_SysID);
            if (Favorites != null)
            {
                return Ok(Favorites);
            }
            return BadRequest();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Favorite value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Favorites.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.FavoriteSysID }, value);
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody]Favorite value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Favorite = await _context.Favorites.FirstOrDefaultAsync(x => x.FavoriteSysID == value.FavoriteSysID);
            if (Favorite == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(UpdateItem), new { id = Favorite.FavoriteSysID }, Favorite);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var Favorite = await _context.Favorites.FirstOrDefaultAsync(x => x.FavoriteSysID == id);
            if (Favorite == null)
            {
                return NotFound();
            }
            _context.Favorites.Remove(Favorite);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
