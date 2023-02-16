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
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        public IActionResult Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(_leaveRequestService.GetLeaveRequests(userId));
        }

        public IActionResult IndexHR()
        {
            return View(_leaveRequestService.GetAllLeaveRequests());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IndexHR(string cardAction, int id)
        {
            var leave = _leaveRequestService.GetLeaveRequestById(id);
            leave.Status = cardAction;
            _leaveRequestService.UpdateLeaveRequest(leave);

            if (leave == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(IndexHR));
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
            ViewData["LeaveTypes"] = new SelectList(_leaveRequestService.GetLeaveTypes());
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,UserId,Type,StartDate,EndDate,Quantity,Status,Comments")] LeaveRequest leaveRequest)
        {
            if (ModelState.IsValid)
            {
                if (leaveRequest.EndDate < leaveRequest.StartDate)
                {
                    ModelState.AddModelError("date", "End Date must be after Start Date.");

                    ViewData["LeaveTypes"] = new SelectList(_leaveRequestService.GetLeaveTypes());
                    return View(leaveRequest);
                }

                leaveRequest.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                leaveRequest.Status = StatusManager.SetStatus();
                _leaveRequestService.CreateLeaveRequest(leaveRequest);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveRequest);
        }

        public IActionResult Edit(int id)
        {
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);
            ViewData["LeaveTypes"] = new SelectList(_leaveRequestService.GetLeaveTypes(leaveRequest.Type));

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
                if (leaveRequest.EndDate < leaveRequest.StartDate)
                {
                    ModelState.AddModelError("date", "End Date must be after Start Date.");

                    ViewData["LeaveTypes"] = new SelectList(_leaveRequestService.GetLeaveTypes());
                    return View(leaveRequest);
                }

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
