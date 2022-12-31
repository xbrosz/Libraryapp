using BL.DTOs.Author;
using BL.DTOs.Branch;
using BL.Facades.Facades;
using BL.Facades.IFacades;
using BL.Services.IServices;
using FE.Models.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers.Admin
{
    public class AdminBranchController : Controller
    {
        private readonly IBranchService _branchService;
        private readonly IBookPrintService _bookPrintService;

        public AdminBranchController(IBranchService branchService, IBookPrintService bookPrintService)
        {
            _branchService = branchService;
            _bookPrintService = bookPrintService;
        }

        public IActionResult Index()
        {
            List<Tuple<BranchDto, bool>> branches = new();

            foreach (var branch in _branchService.GetAll())
            {
                if (_bookPrintService.GetBookPrintsByBranchID(branch.Id).Any())
                {
                    branches.Add(Tuple.Create(branch, false));

                } else
                {
                    branches.Add(Tuple.Create(branch, true));
                }
            }

            return View(new AdminBranchIndexViewModel()
            {
                Branches = branches
            });
        }

        public IActionResult Edit(int branchId)
        {
            var branch = _branchService.Find(branchId);

            return View(new AdminBranchEditViewModel()
            {
                Id = branch.Id,
                Name= branch.Name,
                Address= branch.Address
            });
        }

        [HttpPost]
        public IActionResult Edit(AdminBranchEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _branchService.Update(new BranchDto()
            {
                Id = model.Id,
                Name = model.Name,
                Address = model.Address
            });

            return RedirectToAction("Index", "AdminBranch");
        }

        public IActionResult Add()
        {
            return View(new AdminBranchAddViewModel());
        }

        [HttpPost]
        public IActionResult Add(AdminBranchAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _branchService.Insert(new BranchDto()
            {
                Name = model.Name,
                Address = model.Address
            });

            return RedirectToAction("Index", "AdminBranch");
        }

        public IActionResult Delete(int branchId)
        {
            if (_bookPrintService.GetBookPrintsByBranchID(branchId).Any())
            {
                throw new Exception("Branch cannot be deleted, because bookprints of this branch exist in database!");
            }

            _branchService.Delete(branchId);

            return RedirectToAction("Index", "AdminBranch");
        }
    }
}
