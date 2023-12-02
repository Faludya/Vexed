using Abstractions.Services;
using DataModels;
using DataModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    [Authorize]
    public class ProjectTeamsController : Controller
    {
        private readonly IProjectTeamService _projectTeamService;
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;
        private readonly IUserDetailsService _userDetailsService;
        private readonly Logger _logger;

        public ProjectTeamsController(IProjectTeamService projectTeamService,IProjectService projectService, IUserService userService, Logger logger, IUserDetailsService userDetailsService)
        {
            _projectTeamService = projectTeamService;
            _logger = logger;
            _userService = userService;
            _projectService = projectService;
            _userDetailsService = userDetailsService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _projectTeamService.GetAllProjectTeams());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Projects Teams", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var project = await _projectTeamService.GetProjectTeamById(id);
                if (project == null)
                {
                    return NotFound();
                }

                return View(project);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Team with Id {id}", ex);
                return View("Error");
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var team = new TeamsProjectsVM();
                team.Projects = await _projectService.GetAllProjects();
                team.UserNames = await _userService.GetAllUserNames();
                return View(team);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while opening Create view for Project Team", ex);
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamsProjectsVM memberVM)
        {
            try
            {
                var userDetails = await _userDetailsService.GetUserDetails(memberVM.ProjectTeam.UserId) ?? new UserDetails();
                memberVM.ProjectTeam.UserName = userDetails.FirstName + " " + userDetails.LastName;
                ModelState.Remove("Projects");
                ModelState.Remove("ProjectTeam.Project");
                ModelState.Remove("UserNames");
                ModelState.Remove("ProjectTeam.UserName");
                ModelState.Remove("ProjectTeam.Id");
                if (ModelState.IsValid)
                {
                    await _projectTeamService.CreateProjectTeam(memberVM.ProjectTeam);
                    TempData["SuccessMessage"] = "Member added successfully!";

                    return RedirectToAction(nameof(Index));
                }
                return View(memberVM);
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
                var team = new TeamsProjectsVM();
                var member = await _projectTeamService.GetProjectTeamById(id);
                if (member == null)
                {
                    return NotFound();
                }
                team.Projects = await _projectService.GetAllProjects();
                team.UserNames = await _userService.GetAllUserNames();
                team.ProjectTeam = member;

                return View(team);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while getting Project for user", ex);
                return View("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamsProjectsVM memberVM)
        {
            if (id != memberVM.ProjectTeam.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Projects");
            ModelState.Remove("ProjectTeam.Project");
            ModelState.Remove("UserNames");
            if (ModelState.IsValid)
            {
                try
                {
                    var userDetails = await _userDetailsService.GetUserDetails(memberVM.ProjectTeam.UserId) ?? new UserDetails();
                    memberVM.ProjectTeam.UserName = userDetails.FirstName + " " + userDetails.LastName;

                    await _projectTeamService.UpdateProjectTeam(memberVM.ProjectTeam);
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
            return View(memberVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            if (await _projectTeamService.GetProjectTeamById(id.Value) == null)
            {
                return NotFound();
            }

            try
            {
                var project = await _projectTeamService.GetProjectTeamById(id.Value);
                if (project == null)
                {
                    return NotFound();
                }

                return View(project);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting Project with Id {id.Value}", ex);
                return View("Error");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _projectTeamService.GetProjectTeamById(id) == null)
            {
                return Problem("Entity set 'VexedDbContext.Projet'  is null.");
            }
            try
            {
                var project = await _projectTeamService.GetProjectTeamById(id);
                if (project != null)
                {
                    await _projectTeamService.DeleteProjectTeam(project);
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
