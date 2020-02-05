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
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public OrderController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Orders = await _context.Orders.ToListAsync();
            if (Orders != null)
            {
                return Ok(Orders);
            }
            return BadRequest();
        }
        //// GET api/<controller>/5
        //[HttpGet("GetBookOrders/{bookSysID}")]
        //public async Task<IActionResult> GetRecord(int bookSysID)
        //{
        //    var Book = await _context.Orders.Where(x => x.BookFK_SysID == bookSysID).ToListAsync();
        //    if (Book != null)
        //    {
                
        //        return Ok(Book);
        //    }
        //    return BadRequest();
        //}
        // GET api/<controller>/5
        [HttpPost("GetUserOrders")]
        public async Task<IActionResult> GetUserBookOrders([FromBody]Order value)
        {
            var Orders = await _context.Orders.Where(x => x.AppUserFK_SysID.Equals(value.AppUserFK_SysID)).Include(x => x.Address).ToListAsync();
            if (Orders != null)
            {
                return Ok(Orders);
            }
            return BadRequest();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Order value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Orders.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.OrderSysID }, value);
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody]Order value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderSysID == value.OrderSysID);
            if(Order == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(UpdateItem), new { id = Order.OrderSysID }, Order);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var Order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderSysID == id);
            if(Order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        
        [HttpDelete("DeleteUserOrder")]
        public async Task<IActionResult> DeleteItem([FromBody]Order value)
        {
            var Order = await _context.Orders.FirstOrDefaultAsync(x => x.OrderSysID == value.OrderSysID && x.AppUserFK_SysID.Equals(value.AppUserFK_SysID));
            if(Order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
