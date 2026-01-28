using BusTicketApp.Domain.Models;
using System.Collections.Generic;

namespace BusTicketApp.Service.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAll();
        Ticket? GetById(Guid id);
        Ticket Insert(Ticket ticket);
        Ticket DeleteById(Guid id);

        List<Ticket> GetByRouteId(Guid routeId);

        List<Ticket> GetByUserId(string userId);
    }
}
