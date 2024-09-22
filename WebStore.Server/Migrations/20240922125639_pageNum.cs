using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Server.Migrations
{
    /// <inheritdoc />
    public partial class pageNum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CurrentPage",
                table: "Library",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7c001291-d1a7-42d2-98bd-f7a98c57ce7d", null, "Customer", "CUSTOMER" },
                    { "b2f15822-2aac-467e-ad5d-fedca3ef2aa1", null, "Manager", "MANAGER" },
                    { "e5f5c255-b70e-479d-8024-e3c81b33ae03", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c001291-d1a7-42d2-98bd-f7a98c57ce7d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2f15822-2aac-467e-ad5d-fedca3ef2aa1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e5f5c255-b70e-479d-8024-e3c81b33ae03");

            migrationBuilder.DropColumn(
                name: "CurrentPage",
                table: "Library");

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
    }
}
