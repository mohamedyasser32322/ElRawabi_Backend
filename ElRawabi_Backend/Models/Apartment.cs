using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Models
{
    public enum ApartmentStatus
    {
        Available = 1,
        Reserved = 2,   
        Sold = 3,
        Closed = 4
    }
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
        public decimal Area { get; set; }
        [Required]
        public decimal PricePerMeter { get; set; }
        [Required]
        public ApartmentStatus Status { get; set; } = ApartmentStatus.Available;
        [Required]
        public ApartmentType Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Floor Floor{ get; set; }
        public int FloorId { get; set; }
        public User? Client { get; set; }
        public int? ClientId { get; set; }
    }
}
