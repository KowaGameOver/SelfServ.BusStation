using MediatR;
using SelfServ.BusStation.Application.DTOs;
using SelfServ.BusStation.Application.Interfaces;

namespace SelfServ.BusStation.Application.Features.Trips.Queries
{
    public class GetTripsByIdQuery : IRequest<TripDto> { }
    public class GetTripsByIdQueryHandler : IRequestHandler<GetTripsByIdQuery, TripDto>
    {
        private readonly ITripRepository _tripRepository;
        public GetTripsByIdQueryHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public Task<TripDto> Handle(GetTripsByIdQuery request, CancellationToken cancellationToken)
        {
            var trip = _tripRepository.GetByIdAsync(request);
        }
    }
}
