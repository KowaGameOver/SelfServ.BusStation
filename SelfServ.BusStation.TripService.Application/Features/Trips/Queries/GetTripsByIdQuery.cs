using MediatR;
using SelfServ.BusStation.TripService.Application.DTOs;
using SelfServ.BusStation.TripService.Application.Interfaces;

namespace SelfServ.BusStation.Application.Features.Trips.Queries
{
    public class GetTripsByIdQuery : IRequest<TripDto> 
    {
        public Guid TripId { get; }
        public GetTripsByIdQuery(Guid tripId)
        {
            TripId = tripId;
        }
    }
    public class GetTripsByIdQueryHandler : IRequestHandler<GetTripsByIdQuery, TripDto>
    {
        private readonly ITripRepository _tripRepository;
        public GetTripsByIdQueryHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public async Task<TripDto> Handle(GetTripsByIdQuery request, CancellationToken cancellationToken)
        {
            var trip = await _tripRepository.GetByIdAsync(request.TripId);
            return new TripDto
            {
                ExternalId = trip.ExternalId,
                CityFrom = trip.CityFrom,
                CityTo = trip.CityTo,
                BasePrice = trip.TicketPrice.BasePrice,
                Tax = trip.TicketPrice.Tax,
                Currency = trip.TicketPrice.Currency,
                Schedule = new ScheduleDto
                {
                    DepartureTime = trip.Schedule.DepartureTime,
                    ArrivalTime = trip.Schedule.ArrivalTime,
                    DepartureStation = trip.Schedule.DepartureStation,
                    ArrivalStation = trip.Schedule.ArrivalStation,
                    Stations = trip.Schedule.Stations.Select(s => new StationDto
                    {
                        ExternalId = s.ExternalId,
                        Name = s.Name,
                        Address = s.Address,
                        Latitude = s.Latitude,
                        Longitude = s.Longitude
                    }).ToList()
                }
            };
        }
    }
}
