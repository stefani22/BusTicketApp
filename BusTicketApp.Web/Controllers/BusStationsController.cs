using System;
using BusTicketApp.Domain.Models;
using BusTicketApp.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace BusTicketApp.Web.Controllers
{
    public class BusStationsController : Controller
    {
        private readonly IBusStationService _busStationService;

        public BusStationsController(IBusStationService busStationService)
        {
            _busStationService = busStationService;
        }

        // GET: BusStations
        public IActionResult Index()
        {
            var stations = _busStationService.GetAll();
            return View(stations);
        }

        // GET: BusStations/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busStation = _busStationService.GetById(id.Value);
            if (busStation == null)
            {
                return NotFound();
            }

            return View(busStation);
        }

        // GET: BusStations/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: BusStations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(BusStation busStation)
        {
            // за почеток воопшто да не се замараме со ModelState
            busStation.Id = Guid.NewGuid();
            _busStationService.Insert(busStation);
            return RedirectToAction(nameof(Index));
        }


        // GET: BusStations/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busStation = _busStationService.GetById(id.Value);
            if (busStation == null)
            {
                return NotFound();
            }
            return View(busStation);
        }

        // POST: BusStations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Guid id, BusStation busStation)
        {
            if (id != busStation.Id)
            {
                return NotFound();
            }

            _busStationService.Update(busStation);
            return RedirectToAction(nameof(Index));
        }


        // GET: BusStations/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busStation = _busStationService.GetById(id.Value);
            if (busStation == null)
            {
                return NotFound();
            }

            return View(busStation);
        }

        // POST: BusStations/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _busStationService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BusStationExists(Guid id)
        {
            return _busStationService.GetById(id) != null;
        }
    }
}
