using BusTicketApp.Domain.DTO;
using BusTicketApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BusTicketApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusRouteService _busRouteService;

        public HomeController(IBusRouteService busRouteService)
        {
            _busRouteService = busRouteService;
        }

        // GET: /
        public IActionResult Index(string fromTown, string toTown)
        {
            var model = new RouteSearchViewModel
            {
                FromTown = fromTown,
                ToTown = toTown
            };

            // Ako ima vneseno barem edno pole - filtrirame
            if (!string.IsNullOrWhiteSpace(fromTown) || !string.IsNullOrWhiteSpace(toTown))
            {
                var routes = _busRouteService.GetAll(); 
                if (!string.IsNullOrWhiteSpace(fromTown))
                {
                    var fromLower = fromTown.ToLower();
                    routes = routes
                        .Where(r => r.BusStation != null &&
                                    r.BusStation.Town.ToLower().Contains(fromLower))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(toTown))
                {
                    var toLower = toTown.ToLower();
                    routes = routes
                        .Where(r => r.DestinationTown.ToLower().Contains(toLower))
                        .ToList();
                }

                model.Results = routes;
            }

            return View(model);
        }
    }
}
