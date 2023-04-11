using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vexed.Models;
using Vexed.Models.ViewModels;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class UserEmploymentsController : Controller
    {
        private readonly IUserEmploymentService _userEmploymentService;
        private readonly IUserService _userService;

        public UserEmploymentsController(IUserEmploymentService userEmploymentService, IUserService userService)
        {
            _userEmploymentService = userEmploymentService;
            _userService = userService;
        }

        [Authorize(Roles = "HumanResources")]
        public IActionResult Index()
        {
            return View(_userEmploymentService.GetAllUsersEmployment());
        }

        [Authorize(Roles = "HumanResources, Employee")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var userEmployment = _userEmploymentService.GetUserEmploymentByUserId(userId);
                return View(userEmployment);
            }

            var userEmployments = _userEmploymentService.GetUserEmploymentById((int)id);
            if (userEmployments == null)
            {
                return NotFound();
            }

            return View(userEmployments);
        }

        [Authorize(Roles = "HumanResources")]
        public IActionResult Create()
        {
            EmploymentViewModel employmentVM = new EmploymentViewModel();
            List<UserNameVM> userNameVM = _userService.GetUnassignedUserEmployment();
            List<UserNameVM> superiorUserNames = _userService.GetAllUserNames();

            employmentVM.UserNamesVM = userNameVM;
            employmentVM.SuperiorNamesVM = superiorUserNames;
            ViewData["Users"] = new SelectList(userNameVM);
            ViewData["Superiors"] = new SelectList(superiorUserNames);

            return View(employmentVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public IActionResult Create(EmploymentViewModel employmentVM)
        {
            if (ModelState.IsValid)
            {
                var userEmployment = employmentVM.UserEmployment;
                userEmployment.UserId = Guid.Parse(employmentVM.SelectedUserId);
                userEmployment.SuperiorId = Guid.Parse(employmentVM.SelectedSuperiorId);
                userEmployment.SuperiorName = _userService.GetUserName(employmentVM.SelectedSuperiorId);
                //_userEmploymentService.CreateUserEmployment(userEmployment);
                _userEmploymentService.CreateUserEmployment(userEmployment);
                return RedirectToAction(nameof(Index));
            }
            return View(employmentVM);
        }

        [Authorize(Roles = "HumanResources")]
        public IActionResult Edit(int id)
        {
            var userEmployment = _userEmploymentService.GetUserEmploymentById(id);
            if (userEmployment == null)
            {
                return NotFound();
            }
            return View(userEmployment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public IActionResult Edit(int id, [Bind("Id,UserId,CompanyName,Department,Function,Location,HireDate,HourlyPay,SuperiorName")] UserEmployment userEmployment)
        {
            if (id != userEmployment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userEmploymentService.UpdateUserEmployment(userEmployment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_userEmploymentService.GetUserEmploymentById(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userEmployment);
        }

        [Authorize(Roles = "HumanResources")]
        public IActionResult Delete(int id)
        {
            var userEmployment = _userEmploymentService.GetUserEmploymentById(id);
            if (userEmployment == null)
            {
                return NotFound();
            }

            return View(userEmployment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_userEmploymentService.GetUserEmploymentById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.UsersEmployments'  is null.");
            }
            var userEmployment = _userEmploymentService.GetUserEmploymentById(id);
            if (userEmployment != null)
            {
                _userEmploymentService.DeleteUserEmployment(userEmployment);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
