namespace SelfServ.BusStation.TripService.Domain.Entities
{
    public class Schedule
    {
        public Guid Id { get; private set; }
        public DateTime DepartureTime { get; private set; }
        public DateTime ArrivalTime { get; private set; }
        public string DepartureStation { get; private set; }
        public string ArrivalStation { get; private set; }
        public List<Station> Stations { get; private set; } = new();

        private Schedule() { }

        public Schedule(DateTime departure, DateTime arrival, string departureStation, string arrivalStation)
        {
            Id = Guid.NewGuid();
            DepartureTime = departure;
            ArrivalTime = arrival;
            DepartureStation = departureStation;
            ArrivalStation = arrivalStation;
        }
    }
}
