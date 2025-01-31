using SelfServ.BusStation.TripService.Domain.Common;

namespace SelfServ.BusStation.TripService.Domain.Entities
{
    public class Trip : AggregateRoot
    {
        public Guid TripId { get; private set; }
        public string ExternalId { get; private set; } 
        public string CityFrom { get; private set; }
        public string CityTo { get; private set; }
        public Schedule Schedule { get; private set; }
        public TicketPrice TicketPrice { get; private set; }
        private Trip() { } 
        public Trip(string externalId, string cityFrom, string cityTo, TicketPrice ticketPrice, Schedule schedule)
        {
            TripId = Guid.NewGuid();
            ExternalId = externalId;
            CityFrom = cityFrom;
            CityTo = cityTo;
            TicketPrice = ticketPrice;
            Schedule = schedule;
        }
    }
}
