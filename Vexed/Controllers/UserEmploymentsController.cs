using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vexed.Data;
using Vexed.Models;
using Vexed.Services;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
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

        public IActionResult Details(int id)
        {
            var userEmployment = _userEmploymentService.GetUserEmploymentById(id);
            if (userEmployment == null)
            {
                return NotFound();
            }

            return View(userEmployment);
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
