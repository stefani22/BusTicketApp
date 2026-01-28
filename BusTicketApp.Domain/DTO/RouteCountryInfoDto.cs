using System;

namespace BusTicketApp.Domain.DTO
{
    public class RouteCountryInfoDto
    {
        // ROUTE INFO
        public Guid RouteId { get; set; }
        public string RouteName { get; set; }
        public string FromStationName { get; set; }
        public string FromStationTown { get; set; }
        public string DestinationTown { get; set; }
        public string DestinationCountry { get; set; }
        public DateTime DepartureTime { get; set; }
        public decimal Price { get; set; }
        public int AvailableSeats { get; set; }
        public double DistanceKm { get; set; }
        public double EstimatedHours { get; set; }

        // COUNTRY INFO (from API)
        public string CountryName { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public double PopulationInMillions { get; set; }
        public bool IsBigCountry { get; set; }
    }
}
