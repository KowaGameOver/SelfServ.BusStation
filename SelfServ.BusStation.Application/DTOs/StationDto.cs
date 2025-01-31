namespace SelfServ.BusStation.Application.DTOs
{
    public class StationDto
    {
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
