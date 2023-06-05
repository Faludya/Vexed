using Abstractions.Services;
using DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Text.Json;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly Logger _logger;

        public ProjectsController(IProjectService projectService, IUserService userService, Logger logger)
        {
            _projectService = projectService;
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _projectService.GetAllProjects());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Projects", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var project = await _projectService.GetProjectById(id);
                if (project == null)
                {
                    return NotFound();
                }

                return View(project);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Project with Id {id}", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var project = new Project();
                project.ProjectManagersList = await _userService.GetAllUserNames();
                project.StartDate = DateTime.Now;
                project.EndDate = DateTime.Now;
                return View(project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening Create view for Project", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    project.Technologies = JsonSerializer.Serialize(project.Technologies);
                    project.UsefulLinks = JsonSerializer.Serialize(project.UsefulLinks);
                    await _projectService.CreateProject(project);
                    TempData["SuccessMessage"] = "Project created successfully!";

                    return RedirectToAction(nameof(Index));
                }
                return View(project);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while creating Project", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while creating Project", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
            }
            return View("Error");
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var project = await _projectService.GetProjectById(id);

                if (project == null)
                {
                    return NotFound();
                }
                project.ProjectManagersList = await _userService.GetAllUserNames();

                return View(project);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Project for user", ex);
                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    project.Technologies = JsonSerializer.Serialize(project.Technologies);
                    project.UsefulLinks = JsonSerializer.Serialize(project.UsefulLinks);
                    await _projectService.UpdateProject(project);
                    TempData["SuccessMessage"] = "Project edited successfully!";
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError("Error occurred while creating Project", ex);
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    return View("Error");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error updating Project with Id {id} ", ex);
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again later.");
                    return View("Error");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || await _projectService.GetProjectById(id) == null)
            {
                return NotFound();
            }
            try
            {
                var project = await _projectService.GetProjectById(id);
                if (project == null)
                {
                    return NotFound();
                }

                return View(project);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Project with Id {id}", ex);
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _projectService.GetProjectById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.Projet'  is null.");
            }
            try
            {
                var project = await _projectService.GetProjectById(id);
                if (project != null)
                {
                    await _projectService.DeleteProject(project);
                    TempData["SuccessMessage"] = "Project deleted successfully!";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Error occurred while deleting Project", ex);
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("Error");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while deleting Project for user", ex);
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View("Error");
            }
        }
    }
}
