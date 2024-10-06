namespace CinemaWebApp.ViewModels
{
    public class CinemaDetailsViewModel
    {
        public Guid Id { get; set; } = new Guid();
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public List<MovieProgramViewModel> Movies { get; set; }
            = new List<MovieProgramViewModel>();
    }
}
