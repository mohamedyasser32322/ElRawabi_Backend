using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Models
{
    public class Floor
    {
        public int Id  { get; set; }
        [Required]
        public int FloorNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Building Building { get; set; }
        public int BuildingId { get; set; }
        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}
