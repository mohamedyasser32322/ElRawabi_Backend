using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.Apartment
{
    public class AllApartmentsDto
    {
        public int Id { get; set; }
        public string? ApartmentNumber { get; set; }
        public ApartmentType Type { get; set; }
        public int BuildingId { get; set; }
    }
}
