using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vexed.Models;
using Vexed.Services;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class TimeCardsController : Controller
    {
        private readonly ITimeCardService _timeCardService;

        public TimeCardsController(ITimeCardService timeCardService)
        {
            _timeCardService= timeCardService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _timeCardService.GetTimeCards(userId));
        }

        public async Task<IActionResult> IndexHR()
        {
            return View(await _timeCardService.GetAllTimeCards());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexHR(string statusAction, int id)
        {
            var timeCard = await _timeCardService.GetTimeCardById(id);
            timeCard.Status = StatusManager.SetStatus(timeCard.Status, statusAction);
            await _timeCardService.UpdateTimeCard(timeCard);

            if (timeCard == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(IndexHR));
        }

        public async Task<IActionResult> IndexSuperior()
        {
            var superiorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _timeCardService.GetTimeCardsForSuperior(superiorId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexSuperior(string statusAction, int id)
        {
            var timeCard = await _timeCardService.GetTimeCardById(id);
            timeCard.Status = StatusManager.SetStatus(timeCard.Status, statusAction);
            await _timeCardService.UpdateTimeCard(timeCard);

            if (timeCard == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(IndexSuperior));
        }
        public async Task<IActionResult> Details(int id)
        {
            var timeCard = await _timeCardService.GetTimeCardById(id);
            if (timeCard == null)
            {
                return NotFound();
            }

            return View(timeCard);
        }

        public async Task<IActionResult> Create(string? copyCard)
        {
            if(copyCard != null)
            {
                //TODO: 
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var previousCard = await _timeCardService.GetLastTimeCard(userId);
                ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes(previousCard.Location));

                return View(previousCard);
            }
            ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProjectCode,TaskDetails,Location,StartDate,EndDate,Quantity,Status,Comments")] TimeCard timeCard)
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

                return RedirectToAction(nameof(Index));
            }
 
            return View(timeCard);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var timeCard = await _timeCardService.GetTimeCardById(id);
            ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes(timeCard.Location));

            if (timeCard == null)
            {
                return NotFound();
            }
            return View(timeCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId, SuperiorId, ProjectCode,TaskDetails,Location,StartDate,EndDate,Quantity,Status,Comments")] TimeCard timeCard)
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _timeCardService.GetTimeCardById(id) == null)
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
            return View(timeCard);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var timeCard = await _timeCardService.GetTimeCardById(id);
            if (timeCard == null)
            {
                return NotFound();
            }

            return View(timeCard);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _timeCardService.GetTimeCardById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.TimeCards'  is null.");
            }
            var timeCard = await _timeCardService.GetTimeCardById(id);
            if (timeCard != null)
            {
                await _timeCardService.DeleteTimeCard(timeCard);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
