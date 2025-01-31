namespace SelfServ.BusStation.TripService.Domain.Entities
{
    public class Station : IEquatable<Station>
    {
        public Guid Id { get; private set; }
        public string ExternalId { get; private set; } 
        public string Name { get; private set; }
        public string Address { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        private Station() { }

        public Station(string externalId, string name, string address, double latitude, double longitude)
        {
            Id = Guid.NewGuid();
            ExternalId = externalId;
            Name = name;
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }

        public bool Equals(Station other)
        {
            if (other == null) return false;
            return ExternalId == other.ExternalId;
        }
        public override bool Equals(object obj)
        {
            if (obj is Station otherStation)
                return Equals(otherStation);
            return false;
        }
        public override int GetHashCode()
            => ExternalId.GetHashCode();
    }
}
