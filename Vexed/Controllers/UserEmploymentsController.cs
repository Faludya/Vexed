using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared;
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
        private readonly Logger _logger;

        public UserEmploymentsController(IUserEmploymentService userEmploymentService, IUserService userService, Logger logger)
        {
            _userEmploymentService = userEmploymentService;
            _userService = userService;
            _logger = logger;
        }

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _userEmploymentService.GetAllUsersEmployment());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting User Employment for HR", ex);
                return View("Error");
            }
        }

        [Authorize(Roles = "HumanResources, Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var userEmployment = await _userEmploymentService.GetUserEmploymentByUserId(userId);
                    return View(userEmployment);
                }

                var userEmployments = await _userEmploymentService.GetUserEmploymentById((int)id);
                if (userEmployments == null)
                {
                    return NotFound();
                }

                return View(userEmployments);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting User Employment with Id {id}", ex);
                return View("Error");
            }
        }

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Create()
        {
            try
            {
                EmploymentViewModel employmentVM = new EmploymentViewModel();
                List<UserNameVM> userNameVM = await _userService.GetUnassignedUserEmployment();
                List<UserNameVM> superiorUserNames = await _userService.GetAllUserNames();

                employmentVM.UserNamesVM = userNameVM;
                employmentVM.SuperiorNamesVM = superiorUserNames;
                ViewData["Users"] = new SelectList(userNameVM);
                ViewData["Superiors"] = new SelectList(superiorUserNames);

                return View(employmentVM);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Create view for User Employment", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Create(EmploymentViewModel employmentVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userEmployment = employmentVM.UserEmployment;
                    userEmployment.UserId = Guid.Parse(employmentVM.SelectedUserId);
                    userEmployment.SuperiorId = Guid.Parse(employmentVM.SelectedSuperiorId);
                    userEmployment.SuperiorName = await _userService.GetUserName(employmentVM.SelectedSuperiorId);
                    //_userEmploymentService.CreateUserEmployment(userEmployment);
                    await _userEmploymentService.CreateUserEmployment(userEmployment);
                    return RedirectToAction(nameof(Index));
                }
                return View(employmentVM);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating User Employment", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Contact Information", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }


        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var userEmployment = await _userEmploymentService.GetUserEmploymentById(id);
                if (userEmployment == null)
                {
                    return NotFound();
                }
                return View(userEmployment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Edit view for User Employment", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CompanyName,Department,Function,Location,HireDate,HourlyPay,SuperiorName")] UserEmployment userEmployment)
        {
            if (id != userEmployment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userEmploymentService.UpdateUserEmployment(userEmployment);
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error occurred while editing User Employment", ex);
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    return View("Error");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating User Employment with Id {id} ", ex);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                    return View("Error");
                }
            }
            return View(userEmployment);
        }

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userEmployment = await _userEmploymentService.GetUserEmploymentById(id);
                if (userEmployment == null)
                {
                    return NotFound();
                }

                return View(userEmployment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting User Employment", ex);
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _userEmploymentService.GetUserEmploymentById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.UsersEmployments'  is null.");
            }
            try
            {
                var userEmployment = await _userEmploymentService.GetUserEmploymentById(id);
                if (userEmployment != null)
                {
                    await _userEmploymentService.DeleteUserEmployment(userEmployment);
                }
            
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while deleting User Employment", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting User Employment with Id {id} ", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }
    }
}
