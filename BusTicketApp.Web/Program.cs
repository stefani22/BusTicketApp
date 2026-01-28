using BusTicketApp.Domain.Models;
using BusTicketApp.Repository;
using BusTicketApp.Repository.Implementation;
using BusTicketApp.Repository.Interface;
using BusTicketApp.Service.Implementation;
using BusTicketApp.Service.Interface;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// DbContext (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity
builder.Services.AddDefaultIdentity<AppUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>();




// Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Services (?? ?? ???????? ?????)
builder.Services.AddTransient<IBusRouteService, BusRouteService>();
builder.Services.AddTransient<IBusStationService, BusStationService>();
builder.Services.AddTransient<ITicketService, TicketService>();
builder.Services.AddTransient<IDataFetchService, DataFetchService>();

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
