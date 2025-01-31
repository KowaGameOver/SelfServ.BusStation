using MediatR;
using SelfServ.BusStation.Application.DTOs;
using SelfServ.BusStation.TripService.Application.Interfaces;

namespace SelfServ.BusStation.TripService.Application.Features.Trips.Events
{
    public class TripIntegratedEvent : INotification
    {
        public List<TripDto> Trips { get; }
        public TripIntegratedEvent(List<TripDto> trips)
        {
            Trips = trips;
        }
    }
    public class TripIntegratedEventHandler : INotificationHandler<TripIntegratedEvent>
    {
        private readonly ITripIntegrationService _tripIntegrationService;

        public TripIntegratedEventHandler(ITripIntegrationService tripIntegrationService)
            => _tripIntegrationService = tripIntegrationService;

        public async Task Handle(TripIntegratedEvent notification, CancellationToken cancellationToken)
            => await _tripIntegrationService.IntegrateTripsAsync(notification.Trips);
    }
}
