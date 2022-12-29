using BL.DTOs;
using BL.DTOs.User;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using FE.Models;
using FE.Models.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;

namespace FE.Controllers.Admin
{
    public class AdminUserController : Controller
    {
        private readonly IUserFacade _userFacade;

        public AdminUserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        // validate admin
        public IActionResult Index(string? searchString = null)
        {
            List<UserDetailDto> users;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = _userFacade.GetUsersBySubStringUserName(searchString).ToList();
            }
            else
            {
                users = _userFacade.GetAllUsers().ToList();
            }

            var model = new AdminUserViewModel()
            {
                Users = users
            };

            return View(model);
        }

        public IActionResult SwitchRole(int userId)
        {
            if (userId != int.Parse(User.Identity.Name)) 
            {
                _userFacade.SwitchRoleForUserId(userId);
            }

            return RedirectToAction("Index", "AdminUser");
        }
    }
}
