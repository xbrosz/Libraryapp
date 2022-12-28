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
    public class AdminUserController : BaseController
    {
        private readonly IUserFacade _userFacade;

        public AdminUserController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        // validate admin
        public IActionResult Index(int page = 1, string? searchString = null)
        {
            List<UserDetailDto> users;

            if (!string.IsNullOrEmpty(searchString))
            {
                users = _userFacade.GetUsersBySubStringUserName(searchString, page, PageSize).ToList();
            }
            else
            {
                users = _userFacade.GetAllUsers(page, PageSize).ToList();
            }

            var model = new AdminUserViewModel()
            {
                Users = users,
                Pagination = new PaginationViewModel(page, users.Count(), PageSize)
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
