using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.Models
{
    public class Cinema
    {
        [Key]
        public Guid Id { get; set; } = new Guid();

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Location { get; set; } = null!;
        public virtual ICollection<CinemaMovie> CinemaMovies { get; set; } 
            =new HashSet<CinemaMovie>();
    }
}
