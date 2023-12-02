using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared;
using DataModels;
using Vexed.Services.Abstractions;
using Vexed.Models;

namespace Vexed.Controllers
{
    [Authorize]
    public class TimeCardsController : Controller
    {
        private readonly ITimeCardService _timeCardService;
        private readonly Logger _logger;

        public TimeCardsController(ITimeCardService timeCardService, Logger logger)
        {
            _timeCardService= timeCardService;
            _logger= logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(await _timeCardService.GetTimeCards(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Time Cards for user", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> IndexHR()
        {
            try
            {
                return View(await _timeCardService.GetTimeCardsHR());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Time Cards for HR", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexHR(string statusAction, int id)
        {
            try
            {
                var timeCard = await _timeCardService.GetTimeCardById(id);

                if (timeCard == null)
                {
                    return NotFound();
                }

                timeCard.Status = StatusManager.SetStatus(timeCard.Status!, statusAction);
                await _timeCardService.UpdateTimeCard(timeCard);

                TempData["SuccessMessage"] = "Time card updated successfully!";
                return RedirectToAction(nameof(IndexHR));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while processing Time Cards by HR", ex);
                return View("Error");
            }
        }


        public async Task<IActionResult> IndexSuperior()
        {
            try
            {
                var superiorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(await _timeCardService.GetTimeCardsForSuperior(superiorId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Time Cards for superior", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexSuperior(string statusAction, int id)
        {
            try
            {
                var timeCard = await _timeCardService.GetTimeCardById(id);
                if (timeCard == null)
                {
                    return NotFound();
                }

                timeCard!.Status = StatusManager.SetStatus(timeCard.Status!, statusAction);
                await _timeCardService.UpdateTimeCard(timeCard);

                TempData["SuccessMessage"] = "Time card updated successfully!";

                return RedirectToAction(nameof(IndexSuperior));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while processing Time Cards for user", ex);
                return View("Error");
            }
        }
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var timeCard = await _timeCardService.GetTimeCardById(id);
                if (timeCard == null)
                {
                    return NotFound();
                }

                return View(timeCard);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Time Card with Id {id}", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Create(string? copyCard)
        {
            try
            {
                if (copyCard != null)
                {
                    var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var previousCard = await _timeCardService.GetLastTimeCard(userId);
                    ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes(previousCard.Location));

                    return View(previousCard);
                }
                ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes());
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Create view for Time Cards", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProjectCode,TaskDetails,Location,StartDate,EndDate,DailyHours,TotalHours,Status,Comments")] TimeCard timeCard)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(timeCard.EndDate < timeCard.StartDate)
                    {
                        ModelState.AddModelError("date", "End Date must be after Start Date.");
                    
                        ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes());
                        return View(timeCard);
                    }

                    timeCard.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    await _timeCardService.CreateTimeCard(timeCard);
                    TempData["SuccessMessage"] = "Time card created successfully!";

                    return RedirectToAction(nameof(Index));
                }
 
                return View(timeCard);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating Time Card", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Time Card", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var timeCard = await _timeCardService.GetTimeCardById(id);
                if (timeCard == null)
                {
                    return NotFound();
                }

                ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes(timeCard.Location));

                return View(timeCard);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Eit view for Time Cards", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId, SuperiorId, ProjectCode,TaskDetails,Location,StartDate,EndDate,DailyHours,TotalHours,Status,Comments")] TimeCard timeCard)
        {
            if (id != timeCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (timeCard.EndDate < timeCard.StartDate)
                {
                    ModelState.AddModelError("date", "End Date must be after Start Date.");

                    ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes());
                    return View(timeCard);
                }

                try
                {
                    await _timeCardService.UpdateTimeCard(timeCard);
                    TempData["SuccessMessage"] = "Time card edited successfully!";

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error occurred while editing Time Card", ex);
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    return View("Error");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurred while updating Contact info with Id {id}", ex);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                    return View("Error");
                }
            }
            return View(timeCard);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var timeCard = await _timeCardService.GetTimeCardById(id);
                if (timeCard == null)
                {
                    return NotFound();
                }

                return View(timeCard);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Time Card with Id  {id}", ex);
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _timeCardService.GetTimeCardById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.TimeCards'  is null.");
            }
            try
            {
                var timeCard = await _timeCardService.GetTimeCardById(id);
                if (timeCard != null)
                {
                    await _timeCardService.DeleteTimeCard(timeCard);
                    TempData["SuccessMessage"] = "Time card deleted successfully!";
                }
            
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while deleting Time Card", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while deleting Time Card for user", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }

    }
}
