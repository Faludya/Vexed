using Abstractions.Services;
using DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Security.Claims;
using Vexed.Models;
using Vexed.Services;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    public class QualificationsController : Controller
    {
        private readonly IQualificationService _qualificationService;
        private readonly Logger _logger;
        private readonly IWebHostEnvironment _env;

        public QualificationsController( IQualificationService qualificationService, Logger logger, IWebHostEnvironment env)
        {
            _qualificationService = qualificationService;
            _logger = logger;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(await _qualificationService.GetQualifications(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Contact Information for user", ex);
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            try
            {
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
        public async Task<IActionResult> Create(Qualification qualification, IFormFile attachmentFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (attachmentFile == null || attachmentFile.Length <= 0)
                    {
                        ModelState.AddModelError("attachmentFile", "Please select a file to upload.");
                        return View(qualification);
                    }

                    qualification.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    await _qualificationService.CreateQualification(qualification, attachmentFile);
                    return RedirectToAction(nameof(Index));
                }
                return View(qualification);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating Qualification", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Qualification", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (await _qualificationService.GetQualificationById(id) == null)
            {
                return NotFound();
            }
            try
            {
                var qualification = await _qualificationService.GetQualificationById(id);

                return View(qualification);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Edit view for Qualifications", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Qualification qualification, IFormFile attachmentFile)
        {
            if (id != qualification.Id)
            {
                return NotFound();
            }

            ModelState.Remove("attachmentFile");
            if (ModelState.IsValid)
            {
                try
                {
                    await _qualificationService.UpdateQualification(qualification, attachmentFile);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error occurred while editing Qualification", ex);
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    return View("Error");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating Qualification with Id {id} ", ex);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                    return View("Error");
                }
            }
            return View(qualification);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _qualificationService.GetQualificationById((int)id) == null)
            {
                return NotFound();
            }
            try
            {
                var contactInfo = await _qualificationService.GetQualificationById((int)id);
                if (contactInfo == null)
                {
                    return NotFound();
                }

                return View(contactInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Qualification with Id {id}", ex);
                return View("Error");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _qualificationService.GetQualificationById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.ContactsInfo'  is null.");
            }
            try
            {
                var qualification = await _qualificationService.GetQualificationById(id);
                if (qualification != null)
                {
                    await _qualificationService.DeleteQualification(qualification);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while deleting Qualification", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while deleting Qualification for user", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }
    }
}
 