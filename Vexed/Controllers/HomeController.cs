using Abstractions.Repositories;
using Abstractions.Services;
using DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Shared.ViewModels;
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

        public HomeController(ILogger<HomeController> logger, IToDoService toDoService, IUserService userService)
        {
            _logger = logger;
            _toDoService = toDoService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return View();
            else
                return RedirectToAction("Dashboard");
        }

        public IActionResult Privacy()
        {
            return View();
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
        public IActionResult Dashboard()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var dashboard = new DashboardViewModel();
            dashboard.ToDoList = _toDoService.GetToDoList(userId).Result;
            dashboard.LastCards = _userService.GetLastCards(userId).Result;
            return View(dashboard);
        }

        public IActionResult Calendar()
        {
            return View();
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