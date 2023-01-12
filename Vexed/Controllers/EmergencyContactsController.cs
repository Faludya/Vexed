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
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    public class EmergencyContactsController : Controller
    {
        private readonly IEmergencyContactService _emergencyContactService;

        public EmergencyContactsController(IEmergencyContactService emergencyContactService)
        {
            _emergencyContactService= emergencyContactService;
        }

        public IActionResult Index()
        {
              return View(_emergencyContactService.GetAllEmergencyContacts());
        }

        public IActionResult Details(int id)
        {
            var emergencyContact = _emergencyContactService.GetEmergencyContactById(id);
            if (emergencyContact == null)
            {
                return NotFound();
            }

            return View(emergencyContact);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserId,FirstName,LastName,Relationship,Phone,Email,Address,AdditionalInformation")] EmergencyContact emergencyContact)
        {
            if (ModelState.IsValid)
            {
                emergencyContact.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _emergencyContactService.CreateEmergencyContact(emergencyContact);
                return RedirectToAction(nameof(Index));
            }
            return View(emergencyContact);
        }

        public IActionResult Edit(int id)
        {
            var emergencyContact = _emergencyContactService.GetEmergencyContactById(id);
            if (emergencyContact == null)
            {
                return NotFound();
            }
            return View(emergencyContact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,UserId,FirstName,LastName,Relationship,Phone,Email,Address,AdditionalInformation")] EmergencyContact emergencyContact)
        {
            if (id != emergencyContact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _emergencyContactService.UpdateEmergencyContact(emergencyContact);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_emergencyContactService.GetEmergencyContactById(id) == null)
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

        public IActionResult Delete(int id)
        {
            var emergencyContact = _emergencyContactService.GetEmergencyContactById(id);
            if (emergencyContact == null)
            {
                return NotFound();
            }

            return View(emergencyContact);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_emergencyContactService.GetEmergencyContactById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.EmergencyContacts'  is null.");
            }
            var emergencyContact = _emergencyContactService.GetEmergencyContactById(id);
            if (emergencyContact != null)
            {
                _emergencyContactService.DeleteEmergencyContact(emergencyContact);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
