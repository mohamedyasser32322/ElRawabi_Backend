using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Models
{
    public enum ApartmentType
    {
        GroundFloor = 1,  
        TypicalFloor = 2,
        Roof = 3
    }
    public class Apartment
    {
        public int Id { get; set; }
        [Required,StringLength(10)]
        public string? ApartmentNumber { get; set; }
        [Required]
        public int FloorNumber { get; set; }
        [Required]
        public decimal Area { get; set; }
        [Required]
        public decimal PricePerMeter { get; set; }
        [Required]
        public bool IsSold { get; set; } = false;
        [Required]
        public ApartmentType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Building Building { get; set; }
        public int BuildingId { get; set; }
        public User? User { get; set; }
        public int? ClientId { get; set; }
    }
}
