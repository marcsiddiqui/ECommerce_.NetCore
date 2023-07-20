
using ECommerce_New.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerce_New.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [LogInOnly]
        [AdminOnly]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [LogInOnly]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}