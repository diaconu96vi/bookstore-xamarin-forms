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
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AuthorController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!_context.Authors.Any())
            {
                return BadRequest();
            }
            var authors = await _context.Authors.ToListAsync();
            if (authors != null)
            {
                return Ok(authors);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.SysID == id);
            if (author == null)
            {
                return BadRequest();
            }
            return Ok(author);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Author value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Authors.Any())
            {
                var author = await _context.Authors.FirstOrDefaultAsync(x => x.Name.Equals(value.Name));
                if (author != null)
                {
                    return BadRequest();
                }
            }
            _context.Authors.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.SysID }, value);
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody]Author value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.SysID == value.SysID);
            if (author == null)
            {
                return NotFound();
            }
            author.Name = value.Name;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(UpdateItem), new { id = author.SysID }, author);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(x => x.SysID == id);
            if (author == null)
            {
                return NotFound();
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
