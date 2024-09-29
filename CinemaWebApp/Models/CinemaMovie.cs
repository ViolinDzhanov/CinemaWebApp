namespace CinemaWebApp.Models
{
    public class CinemaMovie
    {
        public Guid CinemaId { get; set; }
        public Cinema Cinema { get; set; } = null!; 
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
    }
}
