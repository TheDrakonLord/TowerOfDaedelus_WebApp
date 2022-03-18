using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TowerOfDaedelus_WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CharSheetAdd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharSheet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalDescriptions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mind = table.Column<int>(type: "int", nullable: true),
                    Strength = table.Column<int>(type: "int", nullable: true),
                    Agility = table.Column<int>(type: "int", nullable: true),
                    Constitution = table.Column<int>(type: "int", nullable: true),
                    Soul = table.Column<int>(type: "int", nullable: true),
                    TraitPoints = table.Column<int>(type: "int", nullable: true),
                    EnergyPoints = table.Column<int>(type: "int", nullable: true),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false),
                    PreviouslyNPC = table.Column<bool>(type: "bit", nullable: false),
                    AvatarURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsReviewed = table.Column<bool>(type: "bit", nullable: false),
                    LastChanged = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharSheet", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharSheet");
        }
    }
}
