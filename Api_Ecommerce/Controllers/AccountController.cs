﻿using Api_Ecommerce.Data;
using Api_Ecommerce.Data.Dtos;
using Api_Ecommerce.Data.Static;
using Api_Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api_Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        [HttpPost]
        [Route("login")]
        public async Task <IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(login.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, login.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                }
                return BadRequest("Wrong credentials");
            }
            return BadRequest("Not Found");


        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register( Register register)
        {
            if(!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(register.EmailAddress);
            if (user != null) 
            {
                ModelState.AddModelError("","User already exists");
                return StatusCode(422, ModelState);
            }
            var newUser = new AppUser
            {
                FullName = register.FullName,
                Email = register.EmailAddress,
                UserName = register.EmailAddress,
                Address = register.Address
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, register.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return Ok("Register Completed");
        }

    }
}
