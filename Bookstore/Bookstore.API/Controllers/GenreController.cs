using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.API.Data;
using Bookstore.API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public GenreController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genres = await _context.Genres.ToListAsync();
            if (genres != null)
            {
                return Ok(genres);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.GenreSysID == id);
            if(genre != null)
            {
                return BadRequest();
            }
            return Ok(genre);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Genre value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = await _context.Genres.FirstOrDefaultAsync(x=> x.Name.Equals(value.GenreSysID));
            if(genre != null)
            {
                return BadRequest();
            }
            _context.Genres.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.GenreSysID }, value);
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody]Genre value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.GenreSysID == value.GenreSysID);
            if(genre == null)
            {
                return NotFound();
            }
            genre.Image = value.Image;
            genre.Name = value.Name;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(UpdateItem), new { id = genre.GenreSysID }, genre);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.GenreSysID == id);
            if(genre == null)
            {
                return NotFound();
            }
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
