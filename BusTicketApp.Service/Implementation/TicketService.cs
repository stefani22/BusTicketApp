using BusTicketApp.Domain.Models;
using BusTicketApp.Repository.Interface;
using BusTicketApp.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BusTicketApp.Service.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;

        public TicketService(IRepository<Ticket> ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public List<Ticket> GetAll()
        {
            return _ticketRepository.GetAll(selector: x => x).ToList();
        }
        public List<Ticket> GetByRouteId(Guid routeId)
        {
            return _ticketRepository
                .GetAll(
                    selector: x => x,
                    predicate: x => x.BusRouteId == routeId,
                    include: q => q
                        .Include(t => t.AppUser)
                        .Include(t => t.BusRoute)
                )
                .ToList();
        }






        public Ticket GetById(Guid id)
        {
            return _ticketRepository.Get(
                selector: x => x,
                predicate: x => x.Id == id,
                include: q => q.Include(t => t.BusRoute)
                               .ThenInclude(r => r.BusStation)
            );
        }

        public Ticket DeleteById(Guid id)
        {
            var entity = GetById(id);

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return _ticketRepository.Delete(entity);
        }


        public Ticket Insert(Ticket ticket)
        {
            return _ticketRepository.Insert(ticket);
        }

        

        public List<Ticket> GetByUserId(string userId)
        {
            return _ticketRepository
                .GetAll(
                    selector: x => x,
                    predicate: x => x.AppUserId == userId,
                    include: q => q.Include(t => t.BusRoute)
                )
                .ToList();
        }



    }
}
