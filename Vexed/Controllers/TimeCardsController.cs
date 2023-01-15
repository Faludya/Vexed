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
        public ActionResult Create([Bind("Id,UserId,ProjectCode,TaskDetails,Location,StartDate,EndDate,Quantity,Status,Comments")] TimeCard timeCard)
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
