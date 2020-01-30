using Bookstore.API.Data;
using Bookstore.API.LookUpEnums;
using Bookstore.API.Models;
using Bookstore.API.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bookstore.API.Controllers
{
    [Route("api/Account")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _applicationDbContext;
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDb)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._applicationDbContext = applicationDb;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users != null)
            {
                return Ok(users);
            }
            return Unauthorized();
        }

        [HttpGet("id")]
        [Route("GetUser")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }
        
        [HttpGet("email")]
        [Route("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }


        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Ok(user);
            }
            return Unauthorized();
        }
        
        [HttpPost]
        [Route("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail([FromBody]LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                return Ok(user);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]SignupModel model)
        {
            if (ModelState.IsValid && model.Password.Equals(model.ConfirmPassword))
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    DtCreated = DateTime.Now,
                    DtModified = DateTime.Now,
                    ActiveUser = true,
                    IsAdmin = model.IsAdmin
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(Enum.GetName(typeof(UserType), UserType.Customer)))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Enum.GetName(typeof(UserType), UserType.Customer)));
                    }

                    if (!await _roleManager.RoleExistsAsync(Enum.GetName(typeof(UserType), UserType.Admin)))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Enum.GetName(typeof(UserType), UserType.Admin)));
                    }
                    if (model.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserType), UserType.Admin));
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(UserType), UserType.Customer));
                    }
                    return Ok(user);
                }
                return BadRequest(result.Errors);
            }
            return BadRequest();
        }

        //[HttpPut]
        //[Route("UpdateUser")]
        //public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel appUser)
        //{
        //    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == appUser.IdUser);
        //    if (user != null)
        //    {
        //        user.Address = appUser.Address;
        //        user.UserName = appUser.UserName;
        //        user.Email = appUser.Email;
        //        user.PhoneNumber = appUser.PhoneNumber;
        //        user.ImageSource = appUser.ImageSource;
        //        user.DtModified = DateTime.Now;
        //        try
        //        {
        //            await _userManager.UpdateAsync(user);
        //            return Ok(user);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}
    }
}
