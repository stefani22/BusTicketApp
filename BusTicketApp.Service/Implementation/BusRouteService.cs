
using BusTicketApp.Domain.Models;
using BusTicketApp.Repository.Interface;
using BusTicketApp.Service.Interface;
using Microsoft.EntityFrameworkCore;
namespace BusTicketApp.Service.Implementation;

public class BusRouteService : IBusRouteService
{
    private readonly IRepository<BusRoute> _routeRepository;

    public BusRouteService(IRepository<BusRoute> routeRepository)
    {
        _routeRepository = routeRepository;
    }

    public List<BusRoute> GetAll()
    {
        return _routeRepository
            .GetAll(
                selector: x => x,
                include: q => q.Include(r => r.BusStation)
            )
            .ToList();
    }

    public BusRoute? GetById(Guid id)
    {
        return _routeRepository.Get(
            selector: x => x,
            predicate: x => x.Id == id
        );
    }

    public BusRoute Insert(BusRoute route)
    {
        return _routeRepository.Insert(route);
    }

    public ICollection<BusRoute> InsertMany(ICollection<BusRoute> routes)
    {
        return _routeRepository.InsertMany(routes);
    }

    public BusRoute Update(BusRoute route)
    {
        return _routeRepository.Update(route);
    }

    public BusRoute DeleteById(Guid id)
    {
        var entity = GetById(id);

        if (entity == null)
        {
            throw new ArgumentNullException("");
        }

        return _routeRepository.Delete(entity);
    }
}
