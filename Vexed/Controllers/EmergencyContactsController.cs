using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared;
using DataModels;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class EmergencyContactsController : Controller
    {
        private readonly IEmergencyContactService _emergencyContactService;
        private readonly Logger _logger;

        public EmergencyContactsController(IEmergencyContactService emergencyContactService, Logger logger)
        {
            _emergencyContactService = emergencyContactService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(await _emergencyContactService.GetEmergencyContacts(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Emergency Contacts for user", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var emergencyContact = await _emergencyContactService.GetEmergencyContactById(id);
                if (emergencyContact == null)
                {
                    return NotFound();
                }

                return View(emergencyContact);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Emergency Contact with Id {id}", ex);
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            try
            {
                ViewData["RelationshipTypes"] = new SelectList(_emergencyContactService.GetRelationshipTypes());
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening Create view for Emergency Contact", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FirstName,LastName,Relationship,Phone,Email,Address,AdditionalInformation")] EmergencyContact emergencyContact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    emergencyContact.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    await _emergencyContactService.CreateEmergencyContact(emergencyContact);
                    TempData["SuccessMessage"] = "Emergency contact created successfully!";

                    return RedirectToAction(nameof(Index));
                }
                return View(emergencyContact);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating Emergency Contact", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Emergency Contact", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var emergencyContact = await _emergencyContactService.GetEmergencyContactById(id);
                ViewData["RelationshipTypes"] = new SelectList(_emergencyContactService.GetRelationshipTypes(emergencyContact.Relationship));

                if (emergencyContact == null)
                {
                    return NotFound();
                }
                return View(emergencyContact);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Emergency Contact for user", ex);
                return View("Error");
            }

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
                    TempData["SuccessMessage"] = "Emergency contact edited successfully!";
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error occurred while creating Emergency Contact", ex);
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    return View("Error");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating Emergency Contact with Id {id} ", ex);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                    return View("Error");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(emergencyContact);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id.HasValue || await _emergencyContactService.GetEmergencyContactById(id.Value) == null)
            {
                return NotFound();
            }
            try
            {
                var emergencyContact = await _emergencyContactService.GetEmergencyContactById(id.Value);
                if (emergencyContact == null)
                {
                    return NotFound();
                }

                return View(emergencyContact);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Emergency Contact with Id {id.Value}", ex);
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _emergencyContactService.GetEmergencyContactById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.EmergencyContacts'  is null.");
            }
            try
            {
                var emergencyContact = await _emergencyContactService.GetEmergencyContactById(id);
                if (emergencyContact != null)
                {
                    await _emergencyContactService.DeleteEmergencyContact(emergencyContact);
                    TempData["SuccessMessage"] = "Emergency contact deleted successfully!";
                }
            
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while deleting Emergency Contact", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while deleting Emergency Contact for user", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }
    }
}
