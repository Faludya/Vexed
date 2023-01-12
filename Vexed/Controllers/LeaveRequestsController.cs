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
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        public IActionResult Index()
        {
              return View(_leaveRequestService.GetAllLeaveRequests());
        }

        public IActionResult Details(int id)
        {
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,UserId,Type,StartDate,EndDate,Quantity,Status,Comments")] LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                leaveRequest.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _leaveRequestService.CreateLeaveRequest(leaveRequest);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveRequest);
        }

        public IActionResult Edit(int id)
        {
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,UserId,Type,StartDate,EndDate,Quantity,Status,Comments")] LeaveRequest leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _leaveRequestService.UpdateLeaveRequest(leaveRequest);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_leaveRequestService.GetLeaveRequestById(id) == null)
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
            return View(leaveRequest);
        }

        public IActionResult Delete(int id)
        {
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);
            if (leaveRequest == null)
            {
                return NotFound();
            }

            return View(leaveRequest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_leaveRequestService.GetLeaveRequestById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.LeaveRequests'  is null.");
            }
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);
            if (leaveRequest != null)
            {
                _leaveRequestService.DeleteLeaveRequest(leaveRequest);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
