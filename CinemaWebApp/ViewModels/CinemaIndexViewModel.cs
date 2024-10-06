using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.ViewModels
{
    public class CinemaIndexViewModel
    {
        public Guid Id { get; set; } = new Guid();

        [Required(ErrorMessage = "Cinema name is required.")]
        [MaxLength(80, ErrorMessage = "Cinema name is too long.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Location is required.")]
        [MaxLength(50, ErrorMessage = "Location name is too long.")]
        public string Location { get; set; } = null!;
    }
}
