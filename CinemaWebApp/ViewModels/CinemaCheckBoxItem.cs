using System.ComponentModel.DataAnnotations;

namespace CinemaWebApp.ViewModels
{
    public class CinemaCheckBoxItem
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsSelected { get; set; }
    }
}
