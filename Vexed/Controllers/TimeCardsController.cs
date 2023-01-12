using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vexed.Data;
using Vexed.Models;

namespace Vexed.Controllers
{
    public class TimeCardsController : Controller
    {
        private readonly VexedDbContext _context;

        public TimeCardsController(VexedDbContext context)
        {
            _context = context;
        }

        // GET: TimeCards
        public async Task<IActionResult> Index()
        {
              return View(await _context.TimeCards.ToListAsync());
        }

        // GET: TimeCards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TimeCards == null)
            {
                return NotFound();
            }

            var timeCard = await _context.TimeCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeCard == null)
            {
                return NotFound();
            }

            return View(timeCard);
        }

        // GET: TimeCards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeCards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProjectCode,TaskDetails,Location,StartDate,EndDate,Quantity,Status,Comments")] TimeCard timeCard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeCard);
        }

        // GET: TimeCards/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TimeCards == null)
            {
                return NotFound();
            }

            var timeCard = await _context.TimeCards.FindAsync(id);
            if (timeCard == null)
            {
                return NotFound();
            }
            return View(timeCard);
        }

        // POST: TimeCards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProjectCode,TaskDetails,Location,StartDate,EndDate,Quantity,Status,Comments")] TimeCard timeCard)
        {
            if (id != timeCard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeCard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeCardExists(timeCard.Id))
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

        // GET: TimeCards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TimeCards == null)
            {
                return NotFound();
            }

            var timeCard = await _context.TimeCards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (timeCard == null)
            {
                return NotFound();
            }

            return View(timeCard);
        }

        // POST: TimeCards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TimeCards == null)
            {
                return Problem("Entity set 'VexedDbContext.TimeCards'  is null.");
            }
            var timeCard = await _context.TimeCards.FindAsync(id);
            if (timeCard != null)
            {
                _context.TimeCards.Remove(timeCard);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeCardExists(int id)
        {
          return _context.TimeCards.Any(e => e.Id == id);
        }
    }
}
