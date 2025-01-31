namespace SelfServ.BusStation.Application.DTOs
{
    public class TripDto
    {
        public string ExternalId { get; set; }
        public string CityFrom { get; set; }
        public string CityTo { get; set; }
        public decimal BasePrice { get; set; }
        public decimal Tax { get; set; }
        public string Currency { get; set; }
        public bool IsRefundable { get; set; }
        public ScheduleDto Schedule { get; set; }
    }
}
