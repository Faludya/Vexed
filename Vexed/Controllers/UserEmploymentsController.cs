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
    public class UserEmploymentsController : Controller
    {
        private readonly VexedDbContext _context;

        public UserEmploymentsController(VexedDbContext context)
        {
            _context = context;
        }

        // GET: UserEmployments
        public async Task<IActionResult> Index()
        {
              return View(await _context.UsersEmployments.ToListAsync());
        }

        // GET: UserEmployments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsersEmployments == null)
            {
                return NotFound();
            }

            var userEmployment = await _context.UsersEmployments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEmployment == null)
            {
                return NotFound();
            }

            return View(userEmployment);
        }

        // GET: UserEmployments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserEmployments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CompanyName,Department,Function,Location,HireDate,HourlyPay,SuperiorName")] UserEmployment userEmployment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userEmployment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userEmployment);
        }

        // GET: UserEmployments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsersEmployments == null)
            {
                return NotFound();
            }

            var userEmployment = await _context.UsersEmployments.FindAsync(id);
            if (userEmployment == null)
            {
                return NotFound();
            }
            return View(userEmployment);
        }

        // POST: UserEmployments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,CompanyName,Department,Function,Location,HireDate,HourlyPay,SuperiorName")] UserEmployment userEmployment)
        {
            if (id != userEmployment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userEmployment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserEmploymentExists(userEmployment.Id))
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
            return View(userEmployment);
        }

        // GET: UserEmployments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsersEmployments == null)
            {
                return NotFound();
            }

            var userEmployment = await _context.UsersEmployments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userEmployment == null)
            {
                return NotFound();
            }

            return View(userEmployment);
        }

        // POST: UserEmployments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsersEmployments == null)
            {
                return Problem("Entity set 'VexedDbContext.UsersEmployments'  is null.");
            }
            var userEmployment = await _context.UsersEmployments.FindAsync(id);
            if (userEmployment != null)
            {
                _context.UsersEmployments.Remove(userEmployment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserEmploymentExists(int id)
        {
          return _context.UsersEmployments.Any(e => e.Id == id);
        }
    }
}
