using CinemaWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApp.Controllers
{
    public class MovieController : Controller
    {
        private static List<Movie> movies = new List<Movie>();
        public IActionResult Index()
        {
            return View(movies);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Create(Movie movie)
        {
            movie.Id = movies.Count + 1;

            movies.Add(movie);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Movie movie = movies.Find(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

    }
}
