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
    private readonly IUserFacade _userFacade;

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
    public async Task<IActionResult> RegisterAsync(UserRegisterViewModel user)
    {
        if (!ModelState.IsValid)
        {
            return View(user);
        }

        try
        {
            _userFacade.Register(new UserCreateDto()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            });
            
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

    [HttpGet("Edit")]
    public IActionResult Edit()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "User");
        }

        var userDetail = _userFacade.GetUserById(int.Parse(User.Identity.Name));

        return View(new UserEditViewModel()
        {
            UserName = userDetail.UserName,
            FirstName = userDetail.FirstName,
            LastName = userDetail.LastName,
            Email = userDetail.Email,
            Address = userDetail.Address,
            PhoneNumber = userDetail.PhoneNumber,
        });
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(UserEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        _userFacade.UpdateUserData(new UserUpdateDto()
        {
            Id = int.Parse(User.Identity.Name),
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            Email = model.Email,
            UserName = model.UserName
        });

        return RedirectToAction("Index", "User");
    }

    [HttpGet("ChangePassword")]
    public IActionResult ChangePassword()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "User");
        }
        return View();
    }

    [HttpPost("ChangePassword")]
    [ValidateAntiForgeryToken]
    public IActionResult ChangePassword(UserChangePasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userChangePasswordDto = new UserChangePasswordDto()
        {
            CurrentPassword = model.CurrentPassword,
            NewPassword = model.NewPassword,
            Id = int.Parse(User.Identity.Name)
        };

        if (!_userFacade.UpdateUserPassword(userChangePasswordDto))
        {
            ModelState.AddModelError("Current password", "Current password is not valid");
            return View(new UserChangePasswordViewModel());
        }

        return RedirectToAction("Index", "User");
        
    }

    [HttpGet("Index")]
    public IActionResult Index()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "User");
        }

        var userDetailDto = _userFacade.GetUserById(int.Parse(User.Identity.Name));

        return View(new UserIndexViewModel()
        {
            UserName= userDetailDto.UserName,
            FirstName= userDetailDto.FirstName,
            LastName= userDetailDto.LastName,
            Email= userDetailDto.Email,
            Address= userDetailDto.Address,
            PhoneNumber= userDetailDto.PhoneNumber,
        });
    }

    public IActionResult Delete() 
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "User");
        }

        _userFacade.DeleteUser(int.Parse(User.Identity.Name));
        Logout();
        return RedirectToAction("Login", "User");
    }

    [HttpGet("Logout")]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync("Cookies");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("Login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(UserLoginViewModel userLogin)
    {
        try { 

            var user = _userFacade.Login(new UserLoginDto()
            {
                UserName = userLogin.UserName,
                Password = userLogin.Password
            });

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
