using Abstractions.Services;
using DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.ViewModels;
using System.Security.Claims;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class SalariesController : Controller
    {
        private readonly ISalaryService _salaryService;
        private readonly Logger _logger;
        private readonly IUserService _userService;
        private readonly IPdfService _pdfService;

        public SalariesController(ISalaryService salaryService, Logger logger, IUserService userService, IPdfService pdfService)
        {
            _salaryService = salaryService;
            _logger = logger;
            _userService = userService;
            _pdfService = pdfService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _salaryService.GetSalaries(DateTime.Now));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Salaries", ex);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index(DateTime selectedDate)
        {
            try
            {
                return View(await _salaryService.GetSalaries(selectedDate));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Salaries", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var salary = await _salaryService.GetSalaryById(id);
                if (salary == null)
                {
                    return NotFound();
                }

                return View(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting Salary with Id {id}", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Create(string selectedUserId)
        {
            try
            {
                var salaryVM = new SalaryVM();
                salaryVM.UserNameVMs = await _userService.GetAllUserNames();

                if (selectedUserId != null && selectedUserId != Guid.Empty.ToString())
                {
                    var salary = new Salary();
                    salary.UserId = Guid.Parse(selectedUserId);
                    salary = await _salaryService.GenerateSalary(salary.UserId);

                    salaryVM.Salary = salary;
                    TempData["SuccessMessage"] = "Salary generated successfully!";
                    return View(salaryVM);
                }

                return View(salaryVM);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating Salary", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Salary", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SalaryVM salaryVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _salaryService.CreateSalary(salaryVM.Salary);
                    TempData["SuccessMessage"] = "Salary created successfully!";

                    return RedirectToAction(nameof(Index));
                }
                return View(salaryVM);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating Contact Information", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Contact Information", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }

        public async Task<IActionResult> GeneratePdf()
        {
            // Generate the PDF using the service
            var salary = await _salaryService.GetSalaryById(1);
            byte[] pdfBytes = _pdfService.GenerateSalaryPdf(salary);

            // Set the response headers
            Response.Headers.Add("Content-Disposition", "attachment; filename=\"myFile.pdf\"");
            Response.Headers.Add("Content-Type", "application/pdf");

            // Write the PDF bytes to the response
            return File(pdfBytes, "application/pdf");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (await _salaryService.GetSalaryById(id) == null)
            {
                return NotFound();
            }
            try
            {
                var salary = await _salaryService.GetSalaryById(id);

                if (salary == null)
                {
                    return NotFound();
                }
                return View(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening the Edit view for Contact Information", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Salary salary)
        {
            if (id != salary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _salaryService.UpdateSalary(salary);
                    TempData["SuccessMessage"] = "Salary edited successfully!";

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error occurred while editing Contact Information", ex);
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    return View("Error");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating Contact Info with Id {id} ", ex);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                    return View("Error");
                }
            }
            return View(salary);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || await _salaryService.GetSalaryById((int)id) == null)
            {
                return NotFound();
            }
            try
            {
                var salary = await _salaryService.GetSalaryById((int)id);
                if (salary == null)
                {
                    return NotFound();
                }

                return View(salary);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting contact information with Id {id}", ex);
                return View("Error");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _salaryService.GetSalaryById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.ContactsInfo'  is null.");
            }
            try
            {
                var salary = await _salaryService.GetSalaryById(id);
                if (salary != null)
                {
                    await _salaryService.DeleteSalary(salary);
                    TempData["SuccessMessage"] = "Salary deleted successfully!";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while deleting Contact Information", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while deleting Contact Information for user", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }
    }
}
