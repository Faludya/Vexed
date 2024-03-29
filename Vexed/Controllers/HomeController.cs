﻿using Abstractions.Services;
using DataModels;
using DataModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Vexed.Models;
using Vexed.Services.Abstractions;

namespace Vexed.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IToDoService _toDoService;
        private readonly IUserService _userService;
        private readonly IProjectTeamService _projectTeamService;
        private readonly ILeaveRequestService _leaveRequestService;

        public HomeController(ILogger<HomeController> logger, IToDoService toDoService, IUserService userService, IProjectTeamService projectTeamService, ILeaveRequestService leaveRequestService)
        {
            _logger = logger;
            _toDoService = toDoService;
            _userService = userService;
            _projectTeamService = projectTeamService;
            _leaveRequestService = leaveRequestService;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Dashboard");
        }

        public async Task<IActionResult> UserProfile(Guid userId)
        {
            var userProfile = await _userService.GetUserProfile(userId);
            return View(userProfile);
        }

        [Authorize]
        public IActionResult Menu()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public async Task<IActionResult> Dashboard()
        {
            Guid userId = default;
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier),out userId);
            var dashboard = new DashboardViewModel();
            dashboard.ToDoList = await _toDoService.GetToDoList(userId);
            dashboard.LastCards = await _userService.GetLastCards(userId);
            dashboard.ProjectTeams = await _projectTeamService.GetUserProjectTeam(userId);
            dashboard.ProjectTeams.OrderByDescending(pt => pt.Project.EndDate);
            dashboard.Salary = new Salary();
            return View(dashboard);
        }

        public async Task<IActionResult> Calendar()
        {
            Guid userId;
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out userId);
            var teamVM = await _leaveRequestService.GetTeamLeaveRequests(userId);
            
            return View(teamVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(string taskText)
        {
            var newTask = new ToDo
            {
                Text = taskText,
                Completed = false,
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            await _toDoService.CreateToDo(newTask);

            // Retrieve the generated ID
            var tasksList = await _toDoService.GetToDoList(newTask.UserId);
            int taskId = tasksList.LastOrDefault().Id;

            // Return the response with the created task and its ID
            return Json(new { id =  taskId, text = newTask.Text });
        }

        [HttpPost]
        public IActionResult DeleteTask(int taskId)
        {
            var task = _toDoService.GetToDoById(taskId).Result;
            _toDoService.DeleteToDo(task);

            // Optionally, you can return a success message or redirect to the updated to-do list view
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTask(int taskId, bool completed)
        {
            var task = _toDoService.GetToDoById(taskId).Result;
            task.Completed = completed;
            await _toDoService.UpdateToDo(task);

            // Optionally, you can return a success message or redirect to the updated to-do list view
            return Json(new { success = true });
        }

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}