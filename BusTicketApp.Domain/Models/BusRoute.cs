using BusTicketApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusTicketApp.Domain.Models;

using BusTicketApp.Domain.Models;
using System.ComponentModel.DataAnnotations;

public class BusRoute : BaseEntity
{
    [Required]
    public string RouteName { get; set; }
    [Required]
    public string DestinationTown { get; set; }
    [Required]

    public DateTime DepartureTime { get; set; }
    [Required]

    public string DestinationCountry { get; set; }
    [Required]

    public decimal Price { get; set; }
    [Required]
    public int AvailableSeats { get; set; }

    public double DistanceKm { get; set; }
    public double EstimatedHours { get; set; }
    [Required]

    public Guid BusStationId { get; set; }

    public BusStation BusStation { get; set; }

    public ICollection<Ticket> Tickets { get; set; }

}


