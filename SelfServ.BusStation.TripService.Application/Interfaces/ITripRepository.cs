using SelfServ.BusStation.TripService.Domain.Entities;

namespace SelfServ.BusStation.TripService.Application.Interfaces
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetAllAsync();
        Task<Trip?> GetByIdAsync(Guid id);
    }
}
