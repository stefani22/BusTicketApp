using System.Net.Http;
using System.Net.Http.Json;
using BusTicketApp.Domain.DTO;
using BusTicketApp.Domain.Models;
using BusTicketApp.Service.Interface;

namespace BusTicketApp.Service.Implementation
{
    public class DataFetchService : IDataFetchService
    {
        private readonly HttpClient _httpClient;

        public DataFetchService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<RouteCountryInfoDto?> GetRouteDetailsWithCountryInfoAsync(BusRoute route)
        {
            if (route == null || string.IsNullOrWhiteSpace(route.DestinationCountry))
                return null;

            var countries = await _httpClient.GetFromJsonAsync<List<CountryApiDto>>(
                "https://api.sampleapis.com/countries/countries");

            if (countries == null)
                return null;

            var country = countries.FirstOrDefault(x =>
                x.Name != null &&
                x.Name.ToLower() == route.DestinationCountry.ToLower());

            if (country == null || country.Population == null)
                return null;

            double populationInMillions = (double)country.Population / 1_000_000;

            var dto = new RouteCountryInfoDto
            {
                // ROUTE INFO
                RouteId = route.Id,
                RouteName = route.RouteName,
                FromStationName = route.BusStation?.Name,
                FromStationTown = route.BusStation?.Town,
                DestinationTown = route.DestinationTown,
                DestinationCountry = route.DestinationCountry,
                DepartureTime = route.DepartureTime,
                Price = route.Price,
                AvailableSeats = route.AvailableSeats,
                DistanceKm = route.DistanceKm,
                EstimatedHours = route.EstimatedHours,

                // COUNTRY INFO
                CountryName = country.Name,
                Capital = country.Capital,
                Region = country.Region,
                PopulationInMillions = Math.Round(populationInMillions, 2),
                IsBigCountry = populationInMillions > 5
            };

            return dto;
        }
    }
}
