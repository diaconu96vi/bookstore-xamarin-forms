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
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public BookController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Books = await _context.Books.ToListAsync();
            if (Books != null)
            {
                return Ok(Books);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var Book = await _context.Books.FirstOrDefaultAsync(x => x.BookSysID == id);
            if(Book != null)
            {
                return BadRequest();
            }
            return Ok(Book);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Book value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Book = await _context.Books.FirstOrDefaultAsync(x=> x.Title.Equals(value.Title));
            if(Book != null)
            {
                return BadRequest();
            }
            _context.Books.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.BookSysID }, value);
        }

        //// PUT api/<controller>/5
        //[HttpPut()]
        //public async Task<IActionResult> UpdateItem([FromBody]Book value)
        //{
        //    if(!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var Book = await _context.Books.FirstOrDefaultAsync(x => x.BookSysID == value.BookSysID);
        //    if(Book == null)
        //    {
        //        return NotFound();
        //    }
        //    Book.Image = value.Image;
        //    Book.Name = value.Name;
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(UpdateItem), new { id = Book.BookSysID }, Book);
        //}

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var Book = await _context.Books.FirstOrDefaultAsync(x => x.BookSysID == id);
            if(Book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(Book);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
