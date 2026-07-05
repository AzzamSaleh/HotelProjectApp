using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelProjectAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApiKeys",
                columns: new[] { "Id", "AppName", "CreatedAtUtc", "ExpiresAtUtc", "Key" },
                values: new object[] { 1, "app", new DateTimeOffset(new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)), null, "dXNlcjFAbG9jYWxob3N0LmNvbTpQQHNzrbd29yZDE=" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36aac992-72ff-4527-9008-52e7c145ca39", "f6e5d4c3-b2a1-4b5c-9d8e-0987654321ba", "User", "USER" },
                    { "c78e8f15-6a6c-4c8a-b5d1-98394b071953", "a1b2c3d4-e5f6-4a5b-8c9d-1234567890ab", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApiKeys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36aac992-72ff-4527-9008-52e7c145ca39");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c78e8f15-6a6c-4c8a-b5d1-98394b071953");
        }
    }
}
