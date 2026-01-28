using System;
using System.Threading.Tasks;
using BusTicketApp.Domain.Models;
using BusTicketApp.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BusTicketApp.Service.Interface;
using BusTicketApp.Domain.DTO;


namespace BusTicketApp.Web.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IBusRouteService _busRouteService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IDataFetchService _dataFetchService;

        public TicketController(
            ITicketService ticketService,
            IBusRouteService busRouteService,
            UserManager<AppUser> userManager,
            IDataFetchService dataFetchService)
        {
            _ticketService = ticketService;
            _busRouteService = busRouteService;
            _userManager = userManager;
            _dataFetchService = dataFetchService;
        }


        public IActionResult Index()
        {
            return RedirectToAction(nameof(MyTickets));
        }

        // GET: /Ticket/MyTickets
        public async Task<IActionResult> MyTickets()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var tickets = _ticketService.GetByUserId(user.Id);
            return View(tickets);
        }

        // GET: /Ticket/Reserve/{routeId}
        [HttpGet]
        public IActionResult Reserve(Guid routeId)
        {
            var route = _busRouteService.GetById(routeId);
            if (route == null)
            {
                return NotFound();
            }

            return View(route); // ќе прикажеме детали за рутата и копче Confirm
        }
        // GET: Ticket/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var ticket = _ticketService.GetById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var route = ticket.BusRoute;

            if (route == null)
            {
                // за секој случај ако не е вклучен
                route = _busRouteService.GetById(ticket.BusRouteId);
            }

            if (route == null)
            {
                return NotFound();
            }

            var model = await _dataFetchService.GetRouteDetailsWithCountryInfoAsync(route);

            if (model == null)
            {
                // нема податоци од API – можеме да прикажеме барем рута
                // или да враќаме view со некоја порака
                // за сега: само рута без држава
                model = new RouteCountryInfoDto
                {
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
                    EstimatedHours = route.EstimatedHours
                };
            }

            return View(model);
        }


        // GET: Ticket/Delete/{id}
        public IActionResult Delete(Guid id)
        {
            var ticket = _ticketService.GetById(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Ticket/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            
            var ticket = _ticketService.GetById(id);
            if (ticket == null)
            {
                return NotFound();
            }
            var currentUserId = _userManager.GetUserId(User);
            if (ticket.AppUserId != currentUserId)
            {
                return Forbid();
            }

            var route = _busRouteService.GetById(ticket.BusRouteId);
            if (route != null)
            {
               
                route.AvailableSeats += 1;
                _busRouteService.Update(route);
            }

            
            _ticketService.DeleteById(id);

            
            return RedirectToAction(nameof(MyTickets));
        }



        // POST: /Ticket/Reserve
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReserveConfirmed(Guid routeId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var route = _busRouteService.GetById(routeId);
            if (route == null)
            {
                return NotFound();
            }

            // Nema sedista
            if (route.AvailableSeats <= 0)
            {
                ModelState.AddModelError("", "No available seats for this route.");
                return View("Reserve", route);
            }

            // Kreiraj bilet
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                BusRouteId = route.Id,
                AppUserId = user.Id,
                BookingDate = DateTime.Now,
                SeatNumber = route.AvailableSeats 
            };

            _ticketService.Insert(ticket);

            
            route.AvailableSeats -= 1;
            _busRouteService.Update(route);

            return RedirectToAction(nameof(MyTickets));
        }

    }
}
