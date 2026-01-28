using BusTicketApp.Domain.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;


namespace BusTicketApp.Repository;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BusRoute>()
            .Property(x => x.Price)
            .HasPrecision(18, 2);
    }


    public DbSet<BusStation> BusStations { get; set; }
    public DbSet<BusRoute> BusRoutes { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
}
