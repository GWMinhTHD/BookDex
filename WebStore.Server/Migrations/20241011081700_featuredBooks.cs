using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Server.Migrations
{
    /// <inheritdoc />
    public partial class featuredBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b29fb99d-7fb8-4d6e-b221-10136597f9eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d7511f53-c8d4-4522-b360-7b0f82cd12ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2663677-3b83-4fe9-ae85-df79d2cf9fbf");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8ed05f0b-02d2-4488-8fcd-e90492d56f20", null, "Manager", "MANAGER" },
                    { "c549aeab-a3a2-4b12-a683-bdf7fcf015d4", null, "Admin", "ADMIN" },
                    { "cb1fba8f-d8b9-4f35-9d7c-4cc74142f18e", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ed05f0b-02d2-4488-8fcd-e90492d56f20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c549aeab-a3a2-4b12-a683-bdf7fcf015d4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb1fba8f-d8b9-4f35-9d7c-4cc74142f18e");

            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Book");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b29fb99d-7fb8-4d6e-b221-10136597f9eb", null, "Customer", "CUSTOMER" },
                    { "d7511f53-c8d4-4522-b360-7b0f82cd12ca", null, "Manager", "MANAGER" },
                    { "e2663677-3b83-4fe9-ae85-df79d2cf9fbf", null, "Admin", "ADMIN" }
                });
        }
    }
}
