using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TowerOfDaedelus_WebApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatePrimaryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CharSheet",
                newName: "CharacterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CharacterID",
                table: "CharSheet",
                newName: "ID");
        }
    }
}
