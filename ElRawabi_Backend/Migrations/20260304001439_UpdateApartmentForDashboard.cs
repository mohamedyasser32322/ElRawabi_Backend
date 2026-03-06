using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElRawabi_Backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApartmentForDashboard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "BuildingsTimeLine",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "BuildingsTimeLine",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Buildings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "BuildingsTimeLine");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "BuildingsTimeLine");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Buildings");
        }
    }
}
