using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Server.Migrations
{
    /// <inheritdoc />
    public partial class adjustAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00d06bc6-672f-4ee3-abfb-9756f6ec9171");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5cd1dd77-c2b0-461f-8215-0309e0775dd4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c18db472-9ac2-4bfc-9c5b-7b23c9f75827");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1849c802-8224-42ef-a864-48bbf9e3679e", null, "Customer", "CUSTOMER" },
                    { "5ba3d08d-8ed8-44bf-8176-27e754f74a1f", null, "Manager", "MANAGER" },
                    { "8efb18da-1c43-4318-b21e-abf078f2814b", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1849c802-8224-42ef-a864-48bbf9e3679e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ba3d08d-8ed8-44bf-8176-27e754f74a1f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8efb18da-1c43-4318-b21e-abf078f2814b");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00d06bc6-672f-4ee3-abfb-9756f6ec9171", null, "Admin", "ADMIN" },
                    { "5cd1dd77-c2b0-461f-8215-0309e0775dd4", null, "Customer", "CUSTOMER" },
                    { "c18db472-9ac2-4bfc-9c5b-7b23c9f75827", null, "Manager", "MANAGER" }
                });
        }
    }
}
