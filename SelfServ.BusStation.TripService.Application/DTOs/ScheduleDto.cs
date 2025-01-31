namespace SelfServ.BusStation.TripService.Application.DTOs
{
    public class ScheduleDto
    {
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string DepartureStation { get; set; }
        public string ArrivalStation { get; set; }
        public List<StationDto> Stations { get; set; } = new();
    }
}
