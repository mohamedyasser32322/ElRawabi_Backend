using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Apartment
{
    public class ApartmentReadDto
    {
        public int Id { get; set; }
        public string? ApartmentNumber { get; set; }
        public int FloorId { get; set; }
        public int FloorNumber { get; set; }
        public int BuildingId { get; set; }
        public string? BuildingNumber { get; set; }
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public decimal Area { get; set; }
        public decimal PricePerMeter { get; set; }
        public ApartmentStatus Status { get; set; }
        public ApartmentType Type { get; set; }
        public string? TypeName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
    }
}