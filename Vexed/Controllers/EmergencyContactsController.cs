using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vexed.Data;
using Vexed.Models;
using Vexed.Services;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class EmergencyContactsController : Controller
    {
        private readonly IEmergencyContactService _emergencyContactService;

        public EmergencyContactsController(IEmergencyContactService emergencyContactService)
        {
            _emergencyContactService= emergencyContactService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _emergencyContactService.GetEmergencyContacts(userId));
        }

        public async Task<IActionResult> Details(int id)
        {
            var emergencyContact = await _emergencyContactService.GetEmergencyContactById(id);
            if (emergencyContact == null)
            {
                return NotFound();
            }

            return View(emergencyContact);
        }

        public IActionResult Create()
        {
            ViewData["RelationshipTypes"] = new SelectList(_emergencyContactService.GetRelationshipTypes());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FirstName,LastName,Relationship,Phone,Email,Address,AdditionalInformation")] EmergencyContact emergencyContact)
        {
            if (ModelState.IsValid)
            {
                emergencyContact.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _emergencyContactService.CreateEmergencyContact(emergencyContact);
                return RedirectToAction(nameof(Index));
            }
            return View(emergencyContact);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var emergencyContact = await _emergencyContactService.GetEmergencyContactById(id);
            ViewData["RelationshipTypes"] = new SelectList(_emergencyContactService.GetRelationshipTypes(emergencyContact.Relationship));

            if (emergencyContact == null)
            {
                return NotFound();
            }
            return View(emergencyContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,FirstName,LastName,Relationship,Phone,Email,Address,AdditionalInformation")] EmergencyContact emergencyContact)
        {
            if (id != emergencyContact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _emergencyContactService.UpdateEmergencyContact(emergencyContact);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _emergencyContactService.GetEmergencyContactById(id) == null)
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
            return View(emergencyContact);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var emergencyContact = await _emergencyContactService.GetEmergencyContactById(id);
            if (emergencyContact == null)
            {
                return NotFound();
            }

            return View(emergencyContact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _emergencyContactService.GetEmergencyContactById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.EmergencyContacts'  is null.");
            }
            var emergencyContact = await _emergencyContactService.GetEmergencyContactById(id);
            if (emergencyContact != null)
            {
                await _emergencyContactService.DeleteEmergencyContact(emergencyContact);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
