using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.BuildingTimeLine
{
    public class BuildingTimeLineCreateDto
    {
        [StringLength(200)]
        public string? Title { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public BuildingStage Stage { get; set; }
        public bool IsCompleted { get; set; }

        [Required]
        public int BuildingId { get; set; }
    }
}
