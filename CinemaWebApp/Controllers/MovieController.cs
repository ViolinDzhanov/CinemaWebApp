using CinemaWebApp.Models;
using CinemaWebApp.Models.Data;
using CinemaWebApp.ViewModels;
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

        public IActionResult Details(Guid id)
        {
            Movie movie = context.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpGet]
        public IActionResult AddToProgram(Guid movieId)
        {
            var movie = context.Movies.Find(movieId);

            if (movie == null)
            {
                return RedirectToAction("Index");
            }

            var cinemas = context.Cinemas.ToList();

            var viewModel = new AddMovieToCinemaProgramViewModel
            {
                MovieId = movie.Id.ToString(),
                MovieTitle = movie.Title,
                Cinemas = cinemas.Select(c => new CinemaCheckBoxItem
                {
                    Id = movie.Id.ToString(),
                    Name = c.Name,
                    IsSelected = false
                })
                .ToList()
            };

            return View(viewModel); 
        }
    }
}
