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

        public IActionResult Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(_timeCardService.GetTimeCards(userId));
        }

        public IActionResult IndexHR()
        {
            return View(_timeCardService.GetAllTimeCards());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IndexHR(string statusAction, int id)
        {
            var timeCard = _timeCardService.GetTimeCardById(id);
            timeCard.Status = StatusManager.SetStatus(timeCard.Status, statusAction);
            _timeCardService.UpdateTimeCard(timeCard);

            if (timeCard == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(IndexHR));
        }

        public IActionResult Details(int id)
        {
            var timeCard = _timeCardService.GetTimeCardById(id);
            if (timeCard == null)
            {
                return NotFound();
            }

            return View(timeCard);
        }

        public IActionResult Create()
        {
            ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserId,ProjectCode,TaskDetails,Location,StartDate,EndDate,Quantity,Status,Comments")] TimeCard timeCard)
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
                timeCard.Status = StatusManager.SetStatus(timeCard.Status);
                _timeCardService.CreateTimeCard(timeCard);

                return RedirectToAction(nameof(Index));
            }
            return View(timeCard);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var timeCard = _timeCardService.GetTimeCardById(id);
            ViewData["LocationTypes"] = new SelectList(_timeCardService.GetLocationTypes(timeCard.Location));

            if (timeCard == null)
            {
                return NotFound();
            }
            return View(timeCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,UserId,ProjectCode,TaskDetails,Location,StartDate,EndDate,Quantity,Status,Comments")] TimeCard timeCard)
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
                    _timeCardService.UpdateTimeCard(timeCard);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_timeCardService.GetTimeCardById(id) == null)
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

        public IActionResult Delete(int id)
        {
            var timeCard = _timeCardService.GetTimeCardById(id);
            if (timeCard == null)
            {
                return NotFound();
            }

            return View(timeCard);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_timeCardService.GetTimeCardById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.TimeCards'  is null.");
            }
            var timeCard = _timeCardService.GetTimeCardById(id);
            if (timeCard != null)
            {
                _timeCardService.DeleteTimeCard(timeCard);
            }
            
            return RedirectToAction(nameof(Index));
        }

    }
}
