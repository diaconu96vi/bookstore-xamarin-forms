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
    public class OrderDetailController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public OrderDetailController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var OrderDetails = await _context.OrderDetails.ToListAsync();
            if (OrderDetails != null)
            {
                return Ok(OrderDetails);
            }
            return BadRequest();
        }
        // GET api/<controller>/5
        [HttpGet("GetBookOrderDetails/{orderSysID}")]
        public async Task<IActionResult> GetRecords(int orderSysID)
        {
            var Book = await _context.OrderDetails.Where(x => x.OrderFK_SysID == orderSysID).Include(x => x.Book).ToListAsync();
            if (Book != null)
            {

                return Ok(Book);
            }
            return BadRequest();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]List<OrderDetail> value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(value != null && value.Any())
            {
                foreach(var orderDetail in value)
                {
                    _context.OrderDetails.Add(orderDetail);
                }
                var result = await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(CreateRecord), value);
            }
            return BadRequest();
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody]OrderDetail value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var OrderDetail = await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderDetailSysID == value.OrderDetailSysID);
            if(OrderDetail == null)
            {
                return NotFound();
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(UpdateItem), new { id = OrderDetail.OrderDetailSysID }, OrderDetail);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var OrderDetail = await _context.OrderDetails.FirstOrDefaultAsync(x => x.OrderDetailSysID == id);
            if(OrderDetail == null)
            {
                return NotFound();
            }
            _context.OrderDetails.Remove(OrderDetail);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
