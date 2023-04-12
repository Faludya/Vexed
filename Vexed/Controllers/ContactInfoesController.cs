using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public ContactInfoesController(UserManager<IdentityUser> userManager, IContactInfoService contactInfoService)
        {
            _userManager = userManager;
            _contactInfoService = contactInfoService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _contactInfoService.GetContactInfos(userId));
        }

        public async Task<IActionResult> Details(int id)
        {
            var contactInfo = await _contactInfoService.GetContactInfoById(id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return View(contactInfo);
        }

        public IActionResult Create()
        {
            ViewData["ContactTypes"] = new SelectList(_contactInfoService.GetContactTypes());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Type,Contact")] ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                contactInfo.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _contactInfoService.CreateContactInfo(contactInfo);
                return RedirectToAction(nameof(Index));
            }
            return View(contactInfo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (await _contactInfoService.GetContactInfoById(id) == null)
            {
                return NotFound();
            }

            var contactInfo = await _contactInfoService.GetContactInfoById(id);
            ViewData["ContactTypes"] = new SelectList(_contactInfoService.GetContactTypes(contactInfo.Type));
            
            if (contactInfo == null)
            {
                return NotFound();
            }
            return View(contactInfo);
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_contactInfoService.GetContactInfoById(id) == null)
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
            return View(contactInfo);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _contactInfoService.GetContactInfoById((int)id) == null)
            {
                return NotFound();
            }

            var contactInfo = await _contactInfoService.GetContactInfoById((int)id);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return View(contactInfo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _contactInfoService.GetContactInfoById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.ContactsInfo'  is null.");
            }
            var contactInfo = await _contactInfoService.GetContactInfoById(id);
            if (contactInfo != null)
            {
                await _contactInfoService.DeleteContactInfo(contactInfo);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
