using BusTicketApp.Domain.Models;

namespace BusTicketApp.Service.Interface;

public interface IBusRouteService
{
    List<BusRoute> GetAll();
    BusRoute? GetById(Guid id);
    BusRoute Insert(BusRoute route);
    ICollection<BusRoute> InsertMany(ICollection<BusRoute> routes);
    BusRoute Update(BusRoute route);
    BusRoute DeleteById(Guid id);
}
