using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DinnerWebApp.Data;
using DinnerWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DinnerWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataRepository _repo;

        public HomeController(ILogger<HomeController> logger, IDataRepository repo)
        {
            _logger = logger;
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<IActionResult> Index()
        {

            ViewBag.DbHealth = await _repo.HealthCheck() ? "healthy" : "Unhealthy";
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
