using System.Collections.Generic;
using BusTicketApp.Domain.Models;

namespace BusTicketApp.Domain.DTO
{
    public class RouteSearchViewModel
    {
        public string FromTown { get; set; }
        public string ToTown { get; set; }

        public List<BusRoute> Results { get; set; } = new List<BusRoute>();
    }
}
