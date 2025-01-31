using SelfServ.BusStation.Application.DTOs;

namespace SelfServ.BusStation.TripService.Application.Interfaces
{
    public interface ITripIntegrationService
    {
        Task IntegrateTripsAsync(List<TripDto> trips);
    }
}
