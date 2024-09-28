using Microsoft.EntityFrameworkCore;

namespace CinemaWebApp.Models.Data
{
    public class CinemaDbContext : DbContext
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            : base(options) 
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
    }
}
