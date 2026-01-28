using BusTicketApp.Domain.Models; 
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusTicketApp.Domain.Models
{
    public class BusStation : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public string Location { get; set; }   

        [Phone]
        public string? PhoneNumber { get; set; }  

        public ICollection<BusRoute> BusRoutes { get; set; }
    }
}
