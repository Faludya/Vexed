using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vexed.Models;
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
        public IActionResult IndexHR(string cardAction, int id)
        {
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        var timeCard = _timeCardService.GetTimeCardById(id);
            //        timeCard.Status = cardAction;
            //        _timeCardService.UpdateTimeCard(timeCard);
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (timeCard == null)
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(IndexHR));
            //}
            //return View(_timeCardService.GetAllTimeCards());
            var timeCard = _timeCardService.GetTimeCardById(id);
            timeCard.Status = cardAction;
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserId,ProjectCode,TaskDetails,Location,StartDate,EndDate,Quantity,Status,Comments")] TimeCard timeCard)
        {
            if (ModelState.IsValid)
            {
                timeCard.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _timeCardService.CreateTimeCard(timeCard);
                return RedirectToAction(nameof(Index));
            }
            return View(timeCard);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var timeCard = _timeCardService.GetTimeCardById(id);
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
