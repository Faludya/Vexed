using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vexed.Models;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class UserDetailsController : Controller
    {
        private readonly IUserDetailsService _userDetailsService;

        public UserDetailsController(IUserDetailsService userDetailsService)
        {
            _userDetailsService= userDetailsService;
        }

        public IActionResult Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(_userDetailsService.GetUsersDetails(userId));
        }

        [Authorize(Roles = "Employee")]
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var userDetail = _userDetailsService.GetUserDetailsByUserId(userId);

                return View(userDetail);
            }

            var userDetails = _userDetailsService.GetUserDetailsById((int)id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserId,FirstName,LastName,PreferredLastName,Gender,DateOfBirth,Nationality,Country,City,Address,AdditionalInformation")] UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                userDetails.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _userDetailsService.CreateUserDetails(userDetails);
                return RedirectToAction(nameof(Index));
            }
            return View(userDetails);
        }

        public IActionResult Edit(int id)
        {
            var userDetails = _userDetailsService.GetUserDetailsById(id);
            if (userDetails == null)
            {
                return NotFound();
            }
            return View(userDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,UserId,FirstName,LastName,PreferredLastName,Gender,DateOfBirth,Nationality,Country,City,Address,AdditionalInformation")] UserDetails userDetails)
        {
            if (id != userDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userDetailsService.UpdateUserDetails(userDetails);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_userDetailsService.GetUserDetailsById(id) == null)
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

        public IActionResult Delete(int id)
        {
            var userDetails = _userDetailsService.GetUserDetailsById(id);
            if (userDetails == null)
            {
                return NotFound();
            }

            return View(userDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_userDetailsService.GetUserDetailsById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.UsersDetails'  is null.");
            }
            var userDetails = _userDetailsService.GetUserDetailsById(id);
            if (userDetails != null)
            {
                _userDetailsService.DeleteUserDetails(userDetails);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
