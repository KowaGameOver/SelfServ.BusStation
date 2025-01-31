using SelfServ.BusStation.Application.DTOs;
using SelfServ.BusStation.TripService.Application.Interfaces;

namespace SelfServ.BusStation.TripService.Infrastructure.Services
{
    internal class TripIntegrationService : ITripIntegrationService
    {
        public Task IntegrateTripsAsync(List<TripDto> trips)
        {
            throw new NotImplementedException();
        }
    }
}
