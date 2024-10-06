using CinemaWebApp.Models;
using CinemaWebApp.Models.Data;
using CinemaWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaWebApp.Controllers
{
    public class CinemaController(CinemaDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var cinemas = context.Cinemas.ToList();

            var cinemaIndex = cinemas.Select(c => new CinemaIndexViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
            });

            return View(cinemaIndex);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CinemaIndexViewModel cinemaIndex)
        {
            if (ModelState.IsValid)
            {
                var cinema = new Cinema
                {
                    Name = cinemaIndex.Name,
                    Location = cinemaIndex.Location,
                };

                context.Cinemas.Add(cinema);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(cinemaIndex);
        }

        public IActionResult Details(Guid id)
        {
            var cinema = context.Cinemas
                .Include(c => c.CinemaMovies)
                .ThenInclude(c => c.Movie)
                .FirstOrDefault(c => c.Id == id);

            if (cinema == null)
            {
                return RedirectToAction("Index");
            }

            var cinemaDetailsViewModel = new CinemaDetailsViewModel
            {
                Id = cinema.Id,
                Name = cinema.Name,
                Location = cinema.Location,
                Movies = cinema.CinemaMovies
                    .Select(cm => new MovieProgramViewModel
                    {
                        Title = cm.Movie.Title,
                        Duration = cm.Movie.Duration,
                    })
                    .ToList()
            };

            return View(cinemaDetailsViewModel);
        }
    }
}
