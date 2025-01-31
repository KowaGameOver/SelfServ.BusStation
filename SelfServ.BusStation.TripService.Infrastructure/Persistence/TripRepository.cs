using SelfServ.BusStation.Application.Interfaces;
using SelfServ.BusStation.TripService.Domain.Entities;

namespace SelfServ.BusStation.TripService.Infrastructure.Persistence
{
    internal class TripRepository : ITripRepository
    {
        public Task<List<Trip>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Trip?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
