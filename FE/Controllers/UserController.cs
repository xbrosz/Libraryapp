using BL.DTOs.User;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using DAL.Entities;
using FE.Models;
using FE.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers;

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
    public async Task<IActionResult> RegisterAsync(UserCreateDto user)
    {
        if (user.Password != user.ConfirmPassword)
        {
            ModelState.AddModelError("Password", "Confirmation password does't match with your password");
            return View("Register");
        }

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

    public IActionResult Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "User");
        }

        var userDetailDto = _userFacade.GetUserById(int.Parse(User.Identity.Name));

        return View(userDetailDto);
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
        try { 

            var user = _userFacade.Login(userLogin);

            await CreateClaimsAndSignInAsync(user);

            return RedirectToAction("Index", "Home");

        } catch (Exception)
        {
            ModelState.AddModelError("Username", "Invalid credentials combination!");
            return View("Login");
        }
    }

    private async Task CreateClaimsAndSignInAsync(UserDetailDto user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.RoleName),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));
    }


}
