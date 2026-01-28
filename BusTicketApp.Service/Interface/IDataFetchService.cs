using System.Threading.Tasks;
using BusTicketApp.Domain.DTO;
using BusTicketApp.Domain.Models;

namespace BusTicketApp.Service.Interface
{
    public interface IDataFetchService
    {
        Task<RouteCountryInfoDto?> GetRouteDetailsWithCountryInfoAsync(BusRoute route);
    }
}
