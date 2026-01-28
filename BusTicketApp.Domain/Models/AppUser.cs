using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BusTicketApp.Domain.Models
{
    public class AppUser : IdentityUser
    {
        
        public string FullName { get; set; }

        
        public ICollection<Ticket> Tickets { get; set; }

        public AppUser()
        {
            Tickets = new List<Ticket>();
        }
    }
}
