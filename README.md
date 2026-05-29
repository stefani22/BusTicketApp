# BusTicketApp

BusTicketApp is a web application developed with ASP.NET Core MVC for managing bus routes and ticket reservations.

## Features

* User registration and login
* Role-based authorization (Admin/User)
* Create, edit, delete, and view bus routes
* Bus station management
* Ticket reservation system
* Seat availability tracking
* SQL Server database integration

## Technologies Used

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* Bootstrap

## Architecture

The application follows the Onion Architecture pattern with separate layers for:

* Domain
* Repository
* Service
* Web

## Getting Started

1. Clone the repository
2. Open the solution in Visual Studio
3. Configure the connection string in `appsettings.json`
4. Run database migrations:

```bash
Update-Database
```

5. Start the application
