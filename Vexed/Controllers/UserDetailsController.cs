﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public UserDetailsController(IUserDetailsService userDetailsService, IUserService userService, UserManager<IdentityUser> userManager)
        {
            _userDetailsService= userDetailsService;
            _userService= userService;
            _userManager= userManager;
        }

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Index()
        {
            return View(await _userDetailsService.GetAllUsersDetails());
        }

        [Authorize(Roles = "HumanResources, Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
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

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Create()
        {
            UsersDetailsViewModel usersDetailsViewModel = new UsersDetailsViewModel();
            List<UserNameVM> userNameVM = await _userService.GetUnassignedUserDetails();

            usersDetailsViewModel.UserNamesVM = userNameVM;
            ViewData["Users"] = new SelectList(userNameVM);

            return View(usersDetailsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Create(UsersDetailsViewModel usersDetailsVM)
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

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Edit(int id)
        {
            var userDetails = await _userDetailsService.GetUserDetailsById(id);
            if (userDetails == null)
            {
                return NotFound();
            }
            return View(userDetails);
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _userDetailsService.GetUserDetailsById(id) == null)
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
            return View(userDetails);
        }

        [Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Delete(int id)
        {
            var userDetails = await _userDetailsService.GetUserDetailsById(id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
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
            var userDetails = await _userDetailsService.GetUserDetailsById(id);
            if (userDetails != null)
            {
                await _userDetailsService.DeleteUserDetails(userDetails);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
