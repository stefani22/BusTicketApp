
using BusTicketApp.Domain.Models;
using BusTicketApp.Repository.Interface;
using BusTicketApp.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace BusTicketApp.Service.Implementation;

public class BusStationService : IBusStationService
{
    private readonly IRepository<BusStation> _stationRepository;

    public BusStationService(IRepository<BusStation> stationRepository)
    {
        _stationRepository = stationRepository;
    }

    public List<BusStation> GetAll()
    {
        return _stationRepository.GetAll(selector: x => x).ToList();
    }

    public BusStation GetById(Guid id)
    {
        return _stationRepository.Get(
            selector: x => x,
            predicate: x => x.Id == id,
            include: q => q
                .Include(bs => bs.BusRoutes)
        );
    }

    public BusStation Insert(BusStation station)
    {
        return _stationRepository.Insert(station);
    }

    public BusStation Update(BusStation station)
    {
        return _stationRepository.Update(station);
    }

    public BusStation DeleteById(Guid id)
    {
        var entity = GetById(id);

        if (entity == null)
        {
            throw new ArgumentNullException("");
        }

        return _stationRepository.Delete(entity);
    }
}
