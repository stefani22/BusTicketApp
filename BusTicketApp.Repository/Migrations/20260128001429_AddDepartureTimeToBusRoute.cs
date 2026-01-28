using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusTicketApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartureTimeToBusRoute : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "BusRoutes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "BusRoutes");
        }
    }
}
