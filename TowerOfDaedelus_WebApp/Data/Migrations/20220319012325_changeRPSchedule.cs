using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TowerOfDaedelus_WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeRPSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "RPSchedule");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "RPSchedule");

            migrationBuilder.AddColumn<int>(
                name: "EndDay",
                table: "RPSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndHour",
                table: "RPSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EndMinute",
                table: "RPSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartDay",
                table: "RPSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "RPSchedule",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartMinute",
                table: "RPSchedule",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDay",
                table: "RPSchedule");

            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "RPSchedule");

            migrationBuilder.DropColumn(
                name: "EndMinute",
                table: "RPSchedule");

            migrationBuilder.DropColumn(
                name: "StartDay",
                table: "RPSchedule");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "RPSchedule");

            migrationBuilder.DropColumn(
                name: "StartMinute",
                table: "RPSchedule");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "RPSchedule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "RPSchedule",
                type: "datetime2",
                nullable: true);
        }
    }
}
