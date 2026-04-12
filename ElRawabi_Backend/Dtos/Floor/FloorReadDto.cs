namespace ElRawabi_Backend.Dtos.Floor
{
    public class FloorReadDto
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public int BuildingId { get; set; }
        public string? BuildingNumber { get; set; }
        public int ApartmentCount { get; set; }
    }
}
