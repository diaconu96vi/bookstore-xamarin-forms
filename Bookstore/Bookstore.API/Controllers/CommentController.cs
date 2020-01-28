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
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CommentController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Comments = await _context.Comments.ToListAsync();
            if (Comments != null)
            {
                return Ok(Comments);
            }
            return BadRequest();
        }

        // GET api/<controller>/5
        [HttpGet("GetUserComments")]
        public async Task<IActionResult> GetUserBookComments([FromBody]Comment value)
        {
            var Comments = await _context.Comments.Where(x => x.AppUserFK_SysID.Equals(value.AppUserFK_SysID)).ToListAsync();
            if (Comments != null)
            {
                return Ok(Comments);
            }
            return BadRequest();
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> CreateRecord([FromBody]Comment value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Comments.Add(value);
            var result = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateRecord), new { id = value.CommentSysID }, value);
        }

        // PUT api/<controller>/5
        [HttpPut()]
        public async Task<IActionResult> UpdateItem([FromBody]Comment value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentSysID == value.CommentSysID);
            if(Comment == null)
            {
                return NotFound();
            }
            Comment.CommentText = value.CommentText;
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(UpdateItem), new { id = Comment.CommentSysID }, Comment);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var Comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentSysID == id);
            if(Comment == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(Comment);
            await _context.SaveChangesAsync();

            return NoContent();

        }
        
        [HttpDelete("DeleteUserComment")]
        public async Task<IActionResult> DeleteItem([FromBody]Comment value)
        {
            var Comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentSysID == value.CommentSysID && x.AppUserFK_SysID.Equals(value.AppUserFK_SysID));
            if(Comment == null)
            {
                return NotFound();
            }
            _context.Comments.Remove(Comment);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
