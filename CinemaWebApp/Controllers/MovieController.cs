using CinemaWebApp.Models;
using CinemaWebApp.Models.Data;
using CinemaWebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApp.Controllers
{
    public class MovieController(CinemaDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var movies = context.Movies.ToList();
            return View(movies);
        } 

        [HttpGet]
        public IActionResult Create()
        {
            return View(new MovieViewModel());
        }

        [HttpPost]
        public IActionResult Create(MovieViewModel movie)
        {
            if(!ModelState.IsValid)
            {
                return View(movie);
            }

            var newMovie = new Movie
            {
                Title = movie.Title,
                Genre = movie.Genre,
                ReleaseDate = movie.ReleaseDate,
                Director = movie.Director,
                Duration = movie.Duration,
                Description = movie.Description,
            };

            context.Movies.Add(newMovie);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Movie movie = context.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

    }
}
