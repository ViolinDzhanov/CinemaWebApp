﻿using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models.ViewModels
{
    public class MovieViewModel
    {
        [Required(ErrorMessage = "Movie title is required.")]
        [StringLength(100, ErrorMessage = "Movie title is too long.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Genre is required.")]
        public string Genre { get; set; } = null!;

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Director name is required.")]
        [StringLength(100, ErrorMessage = "Director name is too long.")]
        public string Director { get; set; } = null!;

        [Required(ErrorMessage = "Please specify the movie duration.")]
        [Range(1, 500, ErrorMessage = "Duration must be between 1 and 500 minutes")]
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
    }
}
