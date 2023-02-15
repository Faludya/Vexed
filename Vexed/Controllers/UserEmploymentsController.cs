using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vexed.Models;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class UserEmploymentsController : Controller
    {
        private readonly IUserEmploymentService _userEmploymentService;

        public UserEmploymentsController(IUserEmploymentService userEmploymentService)
        {
           _userEmploymentService= userEmploymentService;
        }

        public IActionResult Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(_userEmploymentService.GetUsersEmployment(userId));
        }

        [Authorize(Roles = "Employee")]
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserId,CompanyName,Department,Function,Location,HireDate,HourlyPay,SuperiorName")] UserEmployment userEmployment)
        {
            if (ModelState.IsValid)
            {
                userEmployment.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _userEmploymentService.CreateUserEmployment(userEmployment);
                return RedirectToAction(nameof(Index));
            }
            return View(userEmployment);
        }

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
