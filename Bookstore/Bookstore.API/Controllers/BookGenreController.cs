using Bookstore.API.Data;
using Bookstore.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    public class BookGenreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public BookGenreController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var BookGenres = await _context.BookGenres.ToListAsync();
            if (BookGenres != null)
            {
                return Ok(BookGenres);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]BookGenre value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var BookGenre = await _context.BookGenres.FirstOrDefaultAsync(x => x.BookGenreSysID.Equals(value.BookGenreSysID));
            if (BookGenre != null)
            {
                return BadRequest();
            }
            _context.BookGenres.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.BookGenreSysID }, value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var BookGenre = await _context.BookGenres.FirstOrDefaultAsync(x => x.BookGenreSysID == id);
            if (BookGenre == null)
            {
                return NotFound();
            }
            _context.BookGenres.Remove(BookGenre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("Genre/{id}")]
        public async Task<IActionResult> GetByGenre(int id)
        {
            var BookGenres = await _context.BookGenres.Where(x=>x.GenreFK_SysID == id).ToListAsync();
            if (BookGenres != null)
            {
                return Ok(BookGenres);
            }
            return BadRequest();
        }
    }
}
