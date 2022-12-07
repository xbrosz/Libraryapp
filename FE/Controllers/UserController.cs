using BL.Facades.IFacades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BL.DTOs.User;
using DAL.Entities;

namespace FE.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        readonly IUserFacade _userFacade;

        public UserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(CreateUserDto user)
        {
            try
            {
                _userFacade.Register(user);
                return RedirectToAction("Login", "User");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Username", "Account with that username already exists!");
                return View("Register");
            }
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(UserLoginDto userLogin)
        {
            try
            {
                var userId = _userFacade.Login(userLogin);        // async

                await CreateClaimsAndSignInAsync(userId);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Username", "Invalid credentials combination!");
                return View("Login");
            }
        }

        private async Task CreateClaimsAndSignInAsync(int userId)
        {
            var claims = new List<Claim>
        {
            //Set User Identity Name to actual user Id - easier access with user connected operations
            new Claim(ClaimTypes.Name, userId.ToString())
        };
            
            claims.Add(new Claim(ClaimTypes.Role, "User"));
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity));

        }
    }
}
