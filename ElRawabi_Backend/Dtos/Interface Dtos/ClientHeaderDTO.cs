using ElRawabi_Backend.Dtos.BuildingTimeLine;

namespace ElRawabi_Backend.Dtos.Interface_Dtos
{
    public class ClientHeaderDTO
    {
        public string? ClientName {  get; set; }
        public string? ProjectName { get; set; }
        public string? FloorNumber {  get; set; }
        public string? UnitNumber { get; set; }
        public string? DeliveryDate { get; set; }
        public bool AccountStatus { get; set; }

        public List<BuildingTimeLineReadDto>? BuildingTimeLineReadDtos { get; set; } = new();
    }
}
