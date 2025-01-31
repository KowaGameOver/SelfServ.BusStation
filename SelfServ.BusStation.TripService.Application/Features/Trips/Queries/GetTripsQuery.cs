using MediatR;
using SelfServ.BusStation.TripService.Application.DTOs;
using SelfServ.BusStation.TripService.Application.Interfaces;

namespace SelfServ.BusStation.Application.Features.Trips.Queries
{
    public class GetTripsQuery : IRequest<List<TripDto>> { }
    public class GetTripsQueryHandler : IRequestHandler<GetTripsQuery, List<TripDto>>
    {
        private readonly ITripRepository _tripRepository;
        public GetTripsQueryHandler(ITripRepository tripRepository)
        {
            _tripRepository = tripRepository;
        }
        public async Task<List<TripDto>> Handle(GetTripsQuery request, CancellationToken cancellationToken)
        {
            var trips = await _tripRepository.GetAllAsync();
            return trips.Select(trip => new TripDto
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
            }).ToList();
        }
    }
}
