using Bookstore.API.Data;
using Bookstore.API.LookUpEnums;
using Bookstore.API.Models;
using Bookstore.API.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
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
            var verifyEmail = await _userManager.FindByEmailAsync(model.Email);
            if (verifyEmail != null)
            {
                return BadRequest(new List<IdentityError>() { new IdentityError() { Description = "Email already exists" } });
            }
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

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new List<IdentityError>() { new IdentityError() { Description = "User not found!" } });
            }

            if (!await _userManager.CheckPasswordAsync(user, model.OldPassword))
            {
                return BadRequest(new List<IdentityError>() { new IdentityError() { Description = "Old password is wrong!" } });
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(user);
        }

        [HttpPut]
        [Route("ChangeUserDetails")]
        public async Task<IActionResult> ChangeUserDetails([FromBody]UserDetailsModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.OldEmail);
            if (user == null)
            {
                return BadRequest();
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.Email);
            var emailResult = await _userManager.ChangeEmailAsync(user, model.Email, token);
            var nameResult = await _userManager.SetUserNameAsync(user, model.UserName);

            if (!emailResult.Succeeded && !nameResult.Succeeded)
            {
                return BadRequest();
            }
            return Ok(user);
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
