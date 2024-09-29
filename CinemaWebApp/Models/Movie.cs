using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models
{
    public class Movie
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        public string Genre { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Director { get; set; } = null!;

        [Required]
        public int Duration { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; }
            = new HashSet<CinemaMovie>();
    }
}
