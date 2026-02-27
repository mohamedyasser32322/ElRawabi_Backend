using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Apartment
{
    public class ApartmentCreateDto
    {
        [Required, StringLength(10)]
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
        public int? BuildingId { get; set; }
    }
}
