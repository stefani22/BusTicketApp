
using BusTicketApp.Domain.Models;

namespace BusTicketApp.Service.Interface;

public interface IBusStationService
{
    List<BusStation> GetAll();
    BusStation? GetById(Guid id);
    BusStation Insert(BusStation station);
    BusStation Update(BusStation station);
    BusStation DeleteById(Guid id);
}
