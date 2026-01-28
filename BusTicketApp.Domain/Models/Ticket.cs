using BusTicketApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketApp.Domain.Models;

public class Ticket : BaseEntity
{
    

    public Guid BusRouteId { get; set; }
    public BusRoute BusRoute { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

    public DateTime BookingDate { get; set; }

    public int SeatNumber { get; set; }
}

