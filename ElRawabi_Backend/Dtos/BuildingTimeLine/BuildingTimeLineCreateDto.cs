using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.BuildingTimeLine
{
    public class BuildingTimeLineCreateDto
    {
        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public BuildingStage Stage { get; set; }

        [Required]
        public int BuildingId { get; set; }
    }
}
