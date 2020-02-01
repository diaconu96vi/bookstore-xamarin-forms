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
    public class AddressController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AddressController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!_context.Addresses.Any())
            {
                return BadRequest();
            }
            var Addresss = await _context.Addresses.ToListAsync();
            if (Addresss != null)
            {
                return Ok(Addresss);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var Address = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressSysID == id);
            if (Address == null)
            {
                return BadRequest();
            }
            return Ok(Address);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Address value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Addresses.Any())
            {
                var Address = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressTitle.Equals(value.AddressTitle) && x.AppUserFK_SysID.Equals(value.AppUserFK_SysID));
                if (Address != null)
                {
                    return BadRequest();
                }
            }
            _context.Addresses.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.AddressSysID }, value);
        }

        //// PUT api/<controller>/5
        //[HttpPut()]
        //public async Task<IActionResult> UpdateItem([FromBody]Address value)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var Address = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressSysID == value.AddressSysID);
        //    if (Address == null)
        //    {
        //        return NotFound();
        //    }
        //    Address.AddressTitle = value.Name;
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(UpdateItem), new { id = Address.SysID }, Address);
        //}

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var Address = await _context.Addresses.FirstOrDefaultAsync(x => x.AddressSysID == id);
            if (Address == null)
            {
                return NotFound();
            }
            _context.Addresses.Remove(Address);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
