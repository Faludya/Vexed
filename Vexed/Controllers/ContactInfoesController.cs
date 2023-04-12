using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Security.Claims;
using Vexed.Models;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class ContactInfoesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IContactInfoService _contactInfoService;
        private readonly Logger _logger;

        public ContactInfoesController(UserManager<IdentityUser> userManager, IContactInfoService contactInfoService, Logger logger)
        {
            _userManager = userManager;
            _contactInfoService = contactInfoService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(await _contactInfoService.GetContactInfos(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Contact Information for user", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var contactInfo = await _contactInfoService.GetContactInfoById(id);
                if (contactInfo == null)
                {
                    return NotFound();
                }

                return View(contactInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting Contact Info with Id {id}", ex);
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            try
            {
                ViewData["ContactTypes"] = new SelectList(_contactInfoService.GetContactTypes());
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Create view for Contact Info", ex);
                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Type,Contact")] ContactInfo contactInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contactInfo.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    await _contactInfoService.CreateContactInfo(contactInfo);
                    return RedirectToAction(nameof(Index));
                }
                return View(contactInfo);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating Contact Information", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Contact Information", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (await _contactInfoService.GetContactInfoById(id) == null)
            {
                return NotFound();
            }
            try
            {
                var contactInfo = await _contactInfoService.GetContactInfoById(id);
                ViewData["ContactTypes"] = new SelectList(_contactInfoService.GetContactTypes(contactInfo.Type));
            
                if (contactInfo == null)
                {
                    return NotFound();
                }
                return View(contactInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Contact Information", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Type,Contact")] ContactInfo contactInfo)
        {
            if (id != contactInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _contactInfoService.UpdateContactInfo(contactInfo);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error occurred while creating Contact Information", ex);
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    return View("Error");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating contact info with ID {id} ", ex);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                    return View("Error");
                }
            }
            return View(contactInfo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _contactInfoService.GetContactInfoById((int)id) == null)
            {
                return NotFound();
            }
            try
            {
                var contactInfo = await _contactInfoService.GetContactInfoById((int)id);
                if (contactInfo == null)
                {
                    return NotFound();
                }

                return View(contactInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting contact information for user", ex);
                return View("Error");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _contactInfoService.GetContactInfoById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.ContactsInfo'  is null.");
            }
            try
            {
                var contactInfo = await _contactInfoService.GetContactInfoById(id);
                if (contactInfo != null)
                {
                    await _contactInfoService.DeleteContactInfo(contactInfo);
                }
            
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating Contact Information", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting contact information for user", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }
    }
}
