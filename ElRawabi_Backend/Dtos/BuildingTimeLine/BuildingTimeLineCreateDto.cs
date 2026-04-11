using ElRawabi_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace ElRawabi_Backend.Dtos.BuildingTimeLine
{
    public class BuildingTimeLineCreateDto
    {
        [Required]
        public BuildingStage Stage { get; set; }

        [Required]
        public int BuildingId { get; set; }
    }
}
