using CinemaWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CinemaWebApp.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Message = "Welcome to The Cinema Web App!";
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
