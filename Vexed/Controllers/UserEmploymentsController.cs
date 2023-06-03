using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared;
using DataModels.ViewModels;
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
                ViewData["UserRoles"] = _userService.GetUserRoles(userEmployments.UserId).Result;

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
                var rolesList = await _userService.GetAllUserRoles();
    
                employmentVM.UserNamesVM = userNameVM;
                employmentVM.SuperiorNamesVM = superiorUserNames;
                employmentVM.UserRoles = rolesList;
                ViewData["Users"] = new SelectList(userNameVM);
                ViewData["Superiors"] = new SelectList(superiorUserNames);
                ViewData["RolesList"] = new SelectList(rolesList);

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
                ModelState.Remove("UserEmployment.SuperiorName");

                if (ModelState.IsValid)
                {
                    var userEmployment = employmentVM.UserEmployment;
                    userEmployment.UserId = Guid.Parse(employmentVM.SelectedUserId);
                    userEmployment.SuperiorId = Guid.Parse(employmentVM.SelectedSuperiorId);
                    userEmployment.SuperiorName = await _userService.GetUserName(employmentVM.SelectedSuperiorId);
                    
                    await _userEmploymentService.CreateUserEmployment(userEmployment);
                    foreach(var role in employmentVM.UserRoles)
                    {
                        await _userService.CreateUserRole(userEmployment.UserId, role);
                    }
                    TempData["SuccessMessage"] = "Employment created successfully!";

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

                EmploymentViewModel employmentVM = new EmploymentViewModel();
                List<UserNameVM> userNameVM = await _userService.GetAllUserNames();
                List<UserNameVM> superiorUserNames = await _userService.GetAllUserNames();
                var userRoles = await _userService.GetUserRoles(userEmployment.UserId);
                var allRoles = await _userService.GetAllUserRoles();

                // Find the index of the corresponding element in the list
                int index = userNameVM.FindIndex(x => x.UserId == userEmployment.UserId.ToString());

                // If the element exists in the list, remove it and insert it at the beginning
                if (index != -1)
                {
                    var user = userNameVM[index];
                    userNameVM.RemoveAt(index);
                    userNameVM.Insert(0, user);
                }

                // Find the index of the corresponding element in the list
                index = superiorUserNames.FindIndex(x => x.UserId == userEmployment.SuperiorId.ToString());

                // If the element exists in the list, remove it and insert it at the beginning
                if (index != -1)
                {
                    var user = superiorUserNames[index];
                    superiorUserNames.RemoveAt(index);
                    superiorUserNames.Insert(0, user);
                }

                employmentVM.UserNamesVM = userNameVM;
                employmentVM.SuperiorNamesVM = superiorUserNames;
                employmentVM.UserEmployment = userEmployment;
                employmentVM.UserRoles = userRoles;
                employmentVM.AllRoles = allRoles;
                ViewData["Users"] = new SelectList(userNameVM);
                ViewData["Superiors"] = new SelectList(superiorUserNames);
                ViewData["RolesList"] = new SelectList(allRoles);

                return View(employmentVM);
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
        public async Task<IActionResult> Edit(int id, EmploymentViewModel employmentVM)
        {
            if (id != employmentVM.UserEmployment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    employmentVM.UserEmployment.UserId = Guid.Parse(employmentVM.SelectedUserId);
                    employmentVM.UserEmployment.SuperiorId = Guid.Parse(employmentVM.SelectedSuperiorId);

                    await _userEmploymentService.UpdateUserEmployment(employmentVM.UserEmployment);
                    await _userService.UpdateUserRoles(employmentVM.UserEmployment.UserId, employmentVM.UserRoles);
                    TempData["SuccessMessage"] = "Employment edited successfully!";

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
            return View(employmentVM.UserEmployment);
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
                    TempData["SuccessMessage"] = "Employment deleted successfully!";
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

        public IActionResult OrganizationChart(string? userId)
        {
            try
            {
                OrganizationChartViewModel orgChart = new OrganizationChartViewModel();
                if (userId == null)
                {
                    var currentUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    orgChart = _userService.GetOrganizationChart(currentUserId);
                }
                else
                {
                    var selectedUserId = Guid.Parse(userId);
                    orgChart = _userService.GetOrganizationChart(selectedUserId);
                }
                return View(orgChart);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting User Employment for HR", ex);
                return View("Error");
            }
        }
    }
}
