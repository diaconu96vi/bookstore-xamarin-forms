using Bookstore.API.Data;
using Bookstore.API.Models;
using Bookstore.Services;
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
    public class CardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CardController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!_context.Cards.Any())
            {
                return BadRequest();
            }
            var Cards = await _context.Cards.Where(x => string.IsNullOrEmpty(x.CardNumber) == false).ToListAsync();
            if (Cards != null)
            {
                return Ok(Cards);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var Card = await _context.Cards.FirstOrDefaultAsync(x => x.SysID == id);
            if (Card == null)
            {
                return BadRequest();
            }
            return Ok(Card);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Card value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Cards.Any())
            {
                var Card = await _context.Cards.FirstOrDefaultAsync(x => x.CardNumber.Equals(value.CardNumber) && x.AppUserFK_SysID.Equals(value.AppUserFK_SysID));
                if (Card != null && !string.IsNullOrEmpty(Card.CardNumber))
                {
                    return BadRequest();
                }
            }
            _context.Cards.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.SysID }, value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var Card = await _context.Cards.FirstOrDefaultAsync(x => x.SysID == id);
            if (Card == null)
            {
                return NotFound();
            }
            _context.Cards.Remove(Card);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
