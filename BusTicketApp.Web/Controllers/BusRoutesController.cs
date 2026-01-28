using System;
using BusTicketApp.Domain.Models;
using BusTicketApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;




using BusTicketApp.Domain.DTO;


namespace BusTicketApp.Web.Controllers
{
    public class BusRoutesController : Controller
    {
        private readonly IBusRouteService _busRouteService;
        private readonly IBusStationService _busStationService;
        private readonly ITicketService _ticketService;

        public BusRoutesController(
        IBusRouteService busRouteService,
        IBusStationService busStationService,
        ITicketService ticketService)
        {
            _busRouteService = busRouteService;
            _busStationService = busStationService;
            _ticketService = ticketService;
        }


        // GET: BusRoutes
        public IActionResult Index()
        {
            var routes = _busRouteService.GetAll();
            return View(routes);
        }

        // GET: BusRoutes/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = _busRouteService.GetById(id.Value);
            if (route == null)
            {
                return NotFound();
            }

            
            var tickets = _ticketService.GetByRouteId(route.Id);

            var model = new RouteTicketsViewModel
            {
                Route = route,
                Tickets = tickets
            };

            return View(model); 
        }

        // GET: BusRoutes/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Guid? stationId)
        {
            var stations = _busStationService.GetAll();

            ViewData["BusStationId"] = new SelectList(
                stations,
                "Id",
                "Name",
                stationId // ja selektira izbranata stanica
            );

            return View();
        }


        // POST: BusRoutes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult Create(BusRoute busRoute)
        {
            // може и без ModelState за да не те зеза
            busRoute.Id = Guid.NewGuid();
            _busRouteService.Insert(busRoute);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]


        // GET: BusRoutes/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = _busRouteService.GetById(id.Value);
            if (route == null)
            {
                return NotFound();
            }

            var stations = _busStationService.GetAll();
            ViewData["BusStationId"] = new SelectList(stations, "Id", "Name", route.BusStationId);

            return View(route);
        }

        // POST: BusRoutes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult Edit(Guid id, BusRoute busRoute)
        {
            if (id != busRoute.Id)
            {
                return NotFound();
            }

            _busRouteService.Update(busRoute);
            return RedirectToAction(nameof(Index));
        }

        // GET: BusRoutes/Delete/5
        [Authorize(Roles = "Admin")]

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = _busRouteService.GetById(id.Value);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: BusRoutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public IActionResult DeleteConfirmed(Guid id)
        {
            _busRouteService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Reservations(Guid id)
        {
            var route = _busRouteService.GetById(id);
            if (route == null)
            {
                return NotFound();
            }

            var tickets = _ticketService.GetByRouteId(id);

            var model = new RouteTicketsViewModel
            {
                Route = route,
                Tickets = tickets
            };

            return View(model);
        }


        private bool BusRouteExists(Guid id)
        {
            return _busRouteService.GetById(id) != null;
        }
    }
}
