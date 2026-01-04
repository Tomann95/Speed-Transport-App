using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speed.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoadingCountry",
                table: "TransportRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UnloadingCountry",
                table: "TransportRequests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoadingCountry",
                table: "TransportRequests");

            migrationBuilder.DropColumn(
                name: "UnloadingCountry",
                table: "TransportRequests");
        }
    }
}
