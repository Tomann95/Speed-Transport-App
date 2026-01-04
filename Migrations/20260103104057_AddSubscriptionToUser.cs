using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Speed.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionExpiry",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionType",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionExpiry",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SubscriptionType",
                table: "AspNetUsers");
        }
    }
}
