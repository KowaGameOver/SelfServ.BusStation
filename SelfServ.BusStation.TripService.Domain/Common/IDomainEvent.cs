
namespace SelfServ.BusStation.TripService.Domain.Common
{
    public interface IDomainEvent
    {
        Guid EventId { get; }      
        DateTime OccurredOn { get; }
    }
}
