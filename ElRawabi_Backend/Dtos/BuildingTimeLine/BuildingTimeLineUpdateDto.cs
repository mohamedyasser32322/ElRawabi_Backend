using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.BuildingTimeLine
{
    public class BuildingTimeLineUpdateDto
    {
        public int Id { get; set; }

        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
