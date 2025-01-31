using SelfServ.BusStation.TripService.Domain.Common;

namespace SelfServ.BusStation.TripService.Domain.Entities
{
    public class TicketPrice : ValueObject
    {
        public decimal BasePrice { get; private set; }
        public decimal Tax { get; private set; }
        public decimal TotalPrice => BasePrice + Tax;
        public string Currency { get; private set; }
        //public bool IsRefundable { get; private set; }

        private TicketPrice() { }

        public TicketPrice(decimal basePrice, decimal tax, string currency)//bool isRefundable
        {
            BasePrice = basePrice;
            Tax = tax;
            Currency = currency;
            //IsRefundable = isRefundable;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return BasePrice;
            yield return Tax;
            yield return Currency;
            //yield return IsRefundable;
        }
    }
}
