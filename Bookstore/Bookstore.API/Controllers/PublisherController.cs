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
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PublisherController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!_context.Publishers.Any())
            {
                return BadRequest();
            }
            var publishers = await _context.Publishers.ToListAsync();
            if (publishers != null)
            {
                return Ok(publishers);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(x => x.SysID == id);
            if (publisher != null)
            {
                return BadRequest();
            }
            return Ok(publisher);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Publisher value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var publisher = await _context.Publishers.FirstOrDefaultAsync(x => x.SysID == value.SysID);
            if (publisher != null)
            {
                return BadRequest();
            }
            _context.Publishers.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.SysID }, value);
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody]Publisher value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var publisher = await _context.Publishers.FirstOrDefaultAsync(x => x.SysID == value.SysID);
            if (publisher == null)
            {
                return NotFound();
            }
            publisher.Name = value.Name;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(UpdateItem), new { id = publisher.SysID }, publisher);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(x => x.SysID == id);
            if (publisher == null)
            {
                return NotFound();
            }
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
