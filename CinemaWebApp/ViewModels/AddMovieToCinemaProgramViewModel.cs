using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.ViewModels
{
    public class AddMovieToCinemaProgramViewModel
    {
        public string MovieId { get; set; } = null!;
        public string MovieTitle { get; set; } = null!;

        public IList<CinemaCheckBoxItem> Cinemas { get; set; }
            = new List<CinemaCheckBoxItem>();
    }
}
