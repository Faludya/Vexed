using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shared;
using Vexed.Models;
using Vexed.Services;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class LeaveRequestsController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly Logger _logger;

        public LeaveRequestsController(ILeaveRequestService leaveRequestService, Logger logger)
        {
            _leaveRequestService = leaveRequestService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(await _leaveRequestService.GetLeaveRequests(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Leave Requests for user", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> IndexHR()
        {
            try
            {
                return View(await _leaveRequestService.GetLeaveRequestsHR());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Leave Requests for HR", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexHR(string statusAction, int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while processing Leave Request by HR", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> IndexSuperior()
        {
            try
            {
                var superiorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return View(await _leaveRequestService.GetLeaveRequestsForSuperior(superiorId));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Leave Requests for superior", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexSuperior(string statusAction, int id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while processing Leave Request by superior", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
                if (leaveRequest == null)
                {
                    return NotFound();
                }

                return View(leaveRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Leave Request with Id {id}", ex);
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            try
            {
                ViewData["LeaveTypes"] = new SelectList(_leaveRequestService.GetLeaveTypes());
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Create view for Leave Requests", ex);
                return View("Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Id,UserId,Type,StartDate,EndDate,Quantity,Status,Comments")] LeaveRequest leaveRequest)
        {
            try
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
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating Leave Request", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Leave Request", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
                ViewData["LeaveTypes"] = new SelectList(_leaveRequestService.GetLeaveTypes(leaveRequest.Type));

                if (leaveRequest == null)
                {
                    return NotFound();
                }
                return View(leaveRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Edit view for Leave Request for user", ex);
                return View("Error");
            }
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
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error occurred while editing Leave request", ex);
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    return View("Error");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occurred while updating Leave Request with Id {id}", ex);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                    return View("Error");
                }
            }
            return View(leaveRequest);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
                if (leaveRequest == null)
                {
                    return NotFound();
                }

                return View(leaveRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Leave Request for user", ex);
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _leaveRequestService.GetLeaveRequestById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.LeaveRequests'  is null.");
            }
            try
            {
               var leaveRequest = await _leaveRequestService.GetLeaveRequestById(id);
                if (leaveRequest != null)
                {
                    await _leaveRequestService.DeleteLeaveRequest(leaveRequest);
                }
            
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while deleting Leave Request", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while deleting Leave Request for user", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }
    }
}
