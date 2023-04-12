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

        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _leaveRequestService.GetLeaveRequests(userId));
        }

        public async Task<IActionResult> IndexHR()
        {
            return View(await _leaveRequestService.GetAllLeaveRequests());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexHR(string statusAction, int id)
        {
            var leave = await _leaveRequestService.GetLeaveRequestById(id);
            leave.Status = StatusManager.SetStatus(leave.Status, statusAction);
            await _leaveRequestService.UpdateLeaveRequest(leave);

            if (leave == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(IndexHR));
        }

        public async Task<IActionResult> IndexSuperior()
        {
            var superiorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _leaveRequestService.GetLeaveRequestsForSuperior(superiorId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexSuperior(string statusAction, int id)
        {
            var leave = await _leaveRequestService.GetLeaveRequestById(id);
            leave.Status = StatusManager.SetStatus(leave.Status, statusAction);
            await _leaveRequestService.UpdateLeaveRequest(leave);

            if (leave == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(IndexSuperior));
        }

        public async Task<IActionResult> Details(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
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
        public async Task<ActionResult> Create([Bind("Id,UserId,Type,StartDate,EndDate,Quantity,Status,Comments")] LeaveRequest leaveRequest)
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
                leaveRequest.Status = StatusManager.SetStatus(leaveRequest.Status);
                await _leaveRequestService.CreateLeaveRequest(leaveRequest);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveRequest);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
            ViewData["LeaveTypes"] = new SelectList(_leaveRequestService.GetLeaveTypes(leaveRequest.Type));

            if (leaveRequest == null)
            {
                return NotFound();
            }
            return View(leaveRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId, SuperiorId, Type,StartDate,EndDate,Quantity,Status,Comments")] LeaveRequest leaveRequest)
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
                    await _leaveRequestService.UpdateLeaveRequest(leaveRequest);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _leaveRequestService.GetLeaveRequestById(id) == null)
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

        public async Task<IActionResult> Delete(int id)
        {
            var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
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
            if (await _leaveRequestService.GetLeaveRequestById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.LeaveRequests'  is null.");
            }
            var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
            if (leaveRequest != null)
            {
                await _leaveRequestService.DeleteLeaveRequest(leaveRequest);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
