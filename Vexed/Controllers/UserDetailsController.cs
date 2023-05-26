using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.ViewModels.CombinedClasses;
using Vexed.Models;
using Vexed.Models.ViewModels;
using Vexed.Services;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class UserDetailsController : Controller
    {
        private readonly IUserDetailsService _userDetailsService;
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Logger _logger;

        public UserDetailsController(IUserDetailsService userDetailsService, IUserService userService, UserManager<IdentityUser> userManager, Logger logger)
        {
            _userDetailsService= userDetailsService;
            _userService= userService;
            _userManager= userManager;
            _logger= logger;
        }

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _userDetailsService.GetAllUsersDetails());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting User Details for HR", ex);
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
                    var userDetail = await _userDetailsService.GetUserDetailsByUserId(userId);

                    return View(userDetail);
                }

                var userDetails = await _userDetailsService.GetUserDetailsById((int)id);
                if (userDetails == null)
                {
                    return NotFound();
                }

                return View(userDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting User Details with Id {id}", ex);
                return View("Error");
            }
        }

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Create()
        {
            try
            {
                UsersDetailsViewModel usersDetailsViewModel = new UsersDetailsViewModel();
                List<UserNameVM> userNameVM = await _userService.GetUnassignedUserDetails();

                usersDetailsViewModel.UserNamesVM = userNameVM;
                ViewData["Users"] = new SelectList(userNameVM);

                return View(usersDetailsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Create view for User Details", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Create(UsersDetailsViewModel usersDetailsVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userDetails = usersDetailsVM.UserDetails;
                    userDetails.UserId = Guid.Parse(usersDetailsVM.SelectedUserId);
                    await _userDetailsService.CreateUserDetails(userDetails);
                    return RedirectToAction(nameof(Index));
                }
                return View(usersDetailsVM);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating User Details", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating User Details", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var userDetails = await _userDetailsService.GetUserDetailsById(id);
                if (userDetails == null)
                {
                    return NotFound();
                }

                UsersDetailsViewModel usersDetailsViewModel = new UsersDetailsViewModel();
                List<UserNameVM> userNameVM = await _userService.GetAllUserNames();

                // Find the index of the corresponding element in the list
                int index = userNameVM.FindIndex(x => x.UserId == userDetails.UserId.ToString());

                // If the element exists in the list, remove it and insert it at the beginning
                if (index != -1)
                {
                    var user = userNameVM[index];
                    userNameVM.RemoveAt(index);
                    userNameVM.Insert(0, user);
                }

                usersDetailsViewModel.UserNamesVM = userNameVM;
                usersDetailsViewModel.UserDetails = userDetails;
                ViewData["Users"] = new SelectList(userNameVM);

                return View(usersDetailsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Edit view for User Details", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,FirstName,LastName,PreferredLastName,Gender,DateOfBirth,Nationality,Country,City,Address,AdditionalInformation")] UserDetails userDetails)
        {
            if (id != userDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userDetailsService.UpdateUserDetails(userDetails);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error occurred while editing User Details", ex);
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    return View("Error");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurred while updating User Details with Id {id}", ex);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                    return View("Error");
                }
            }
            return View(userDetails);
        }

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userDetails = await _userDetailsService.GetUserDetailsById(id);
                if (userDetails == null)
                {
                    return NotFound();
                }

                return View(userDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting User Details with Id {id}", ex);
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _userDetailsService.GetUserDetailsById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.UsersDetails'  is null.");
            }
            try
            {
                var userDetails = await _userDetailsService.GetUserDetailsById(id);
                if (userDetails != null)
                {
                    await _userDetailsService.DeleteUserDetails(userDetails);
                }
            
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while deleting User Details", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while deleting User Details", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }
    }
}
