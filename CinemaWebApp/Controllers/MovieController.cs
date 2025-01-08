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
            Movie movie = context.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpGet]
        public IActionResult AddToProgram(Guid id)
        {
            Movie movie = context.Movies.Find(id);

            if (movie is null)
            {
                return RedirectToAction("Index");
            }

            var cinemas = context.Cinemas.ToList();

            var cinemaMovies = context.CinemasMovies
                .Where(cm => cm.MovieId == movie.Id)
                .Select(cm => cm.CinemaId)
                .ToList();

            var viewModel = new AddMovieToCinemaProgramViewModel
            {
                MovieId = movie.Id.ToString(),
                MovieTitle = movie.Title,
                Cinemas = cinemas.Select(c => new CinemaCheckBoxItem
                {
                    Id = c.Id.ToString(),
                    Name = c.Name,
                    IsSelected = false
                })
                .ToList()
            };

            foreach (var cinema in viewModel.Cinemas)
            {
                if (cinemaMovies.Contains(Guid.Parse(cinema.Id)))
                {
                    cinema.IsSelected = true;
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddToProgram(AddMovieToCinemaProgramViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var movie = context.Movies.Find(Guid.Parse(viewModel.MovieId));

            if (movie is null)
            {
                return RedirectToAction("Index");
            }

            var selectedCinemas = viewModel.Cinemas
                .Where(c => c.IsSelected)
                .Select(c => Guid.Parse(c.Id))
                .ToList();

           

            // Determine which cinemas to remove (deselected cinemas)
         

            // Remove the movie from deselected cinemas
            //var cinemasMoviesToRemove = context.CinemasMovies
            //    .Where(cm => cm.MovieId == movie.Id && deselectedCinemas.Contains(cm.CinemaId))
            //    .ToList();

            //context.CinemasMovies.RemoveRange(cinemasMoviesToRemove);

            foreach (var cinemaId in selectedCinemas)
            {
                var cinema = context.Cinemas.Find(cinemaId);
                if (cinema is null )
                {
                    continue;
                }
                var cinemaMovie = new CinemaMovie
                {
                    Cinema = cinema,
                    Movie = movie
                };

                var existingCinemaMovie = context.CinemasMovies
                    .Where(cm => cm.MovieId == movie.Id && cm.CinemaId == cinema.Id)
                    .FirstOrDefault();

                if (existingCinemaMovie != null)
                {
                    continue;
                }

                context.CinemasMovies.Add(cinemaMovie);
            }

            var deselectedCinemas = viewModel.Cinemas
             .Where(c => !c.IsSelected)
             .ToList();

            foreach (var cinema in deselectedCinemas)
            {
                var cinemaMovie = context.CinemasMovies
                    .Where(cm => cm.MovieId == movie.Id && cm.CinemaId == Guid.Parse(cinema.Id))
                    .FirstOrDefault();
                if (cinemaMovie is not null)
                {
                    context.CinemasMovies.Remove(cinemaMovie);
                }
            }

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
