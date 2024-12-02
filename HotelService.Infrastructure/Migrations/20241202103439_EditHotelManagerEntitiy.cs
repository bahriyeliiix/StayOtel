using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditHotelManagerEntitiy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "HotelManagers",
                newName: "PhoneNumber");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "HotelManagers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "HotelManagers");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "HotelManagers",
                newName: "Position");
        }
    }
}
