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
    public class EmergencyContactsController : Controller
    {
        private readonly VexedDbContext _context;

        public EmergencyContactsController(VexedDbContext context)
        {
            _context = context;
        }

        // GET: EmergencyContacts
        public async Task<IActionResult> Index()
        {
              return View(await _context.EmergencyContacts.ToListAsync());
        }

        // GET: EmergencyContacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmergencyContacts == null)
            {
                return NotFound();
            }

            var emergencyContact = await _context.EmergencyContacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emergencyContact == null)
            {
                return NotFound();
            }

            return View(emergencyContact);
        }

        // GET: EmergencyContacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmergencyContacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,FirstName,LastName,Relationship,Phone,Email,Address,AdditionalInformation")] EmergencyContact emergencyContact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emergencyContact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emergencyContact);
        }

        // GET: EmergencyContacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmergencyContacts == null)
            {
                return NotFound();
            }

            var emergencyContact = await _context.EmergencyContacts.FindAsync(id);
            if (emergencyContact == null)
            {
                return NotFound();
            }
            return View(emergencyContact);
        }

        // POST: EmergencyContacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(emergencyContact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmergencyContactExists(emergencyContact.Id))
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

        // GET: EmergencyContacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmergencyContacts == null)
            {
                return NotFound();
            }

            var emergencyContact = await _context.EmergencyContacts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emergencyContact == null)
            {
                return NotFound();
            }

            return View(emergencyContact);
        }

        // POST: EmergencyContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmergencyContacts == null)
            {
                return Problem("Entity set 'VexedDbContext.EmergencyContacts'  is null.");
            }
            var emergencyContact = await _context.EmergencyContacts.FindAsync(id);
            if (emergencyContact != null)
            {
                _context.EmergencyContacts.Remove(emergencyContact);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmergencyContactExists(int id)
        {
          return _context.EmergencyContacts.Any(e => e.Id == id);
        }
    }
}
