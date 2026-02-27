using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Apartment
{
    public class ApartmentUpdateDto
    {
        public int Id { get; set; }
        [StringLength(10)]
        public string? ApartmentNumber { get; set; }
        public int FloorNumber { get; set; }
        public decimal Area { get; set; }
        public decimal PricePerMeter { get; set; }
        public bool IsSold { get; set; }
        public ApartmentType Type { get; set; }
        public int BuildingId { get; set; }
        public int? ClientId { get; set; }
    }
}
