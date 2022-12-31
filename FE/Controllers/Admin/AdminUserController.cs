using BL.DTOs;
using BL.DTOs.User;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using FE.Models;
using FE.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing.Printing;

namespace FE.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly IUserFacade _userFacade;
        private readonly IReservationFacade _reservationFacade;

        public AdminUserController(IUserFacade userFacade, IReservationFacade reservationFacade)
        {
            _userFacade = userFacade;
            _reservationFacade = reservationFacade;
        }

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

        [ValidateAntiForgeryToken]
        public IActionResult SwitchRole(int userId)
        {
            if (userId == int.Parse(User.Identity.Name)) 
            {
                throw new Exception("You cannot change your own role!");
            }

            _userFacade.SwitchRoleForUserId(userId);

            return RedirectToAction("Index", "AdminUser");
        }

        [ValidateAntiForgeryToken]
        public IActionResult DeleteUser(int userId)
        {
            _userFacade.DeleteUser(userId);
            _reservationFacade.DeleteReservationsForUserId(userId);

            if (userId == int.Parse(User.Identity.Name))
            {
                return RedirectToAction("Logout", "User");
            }

            return RedirectToAction("Index", "AdminUser");
        }
    }
}
