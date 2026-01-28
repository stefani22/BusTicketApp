using System.Collections.Generic;
using BusTicketApp.Domain.Models;

namespace BusTicketApp.Domain.DTO
{
    public class RouteTicketsViewModel
    {
        public BusRoute Route { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
