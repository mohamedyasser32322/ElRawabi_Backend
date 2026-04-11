using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Apartment
{
    public class ApartmentReadDto
    {
        public int Id { get; set; }
        public string? ApartmentNumber { get; set; }
        public int FloorNumber { get; set; }
        public decimal Area { get; set; }
        public decimal PricePerMeter { get; set; }
        public decimal TotalPrice => Area * PricePerMeter;
        public bool IsSold { get; set; } = false;
        public ApartmentType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}