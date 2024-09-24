using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Server.Migrations
{
    /// <inheritdoc />
    public partial class removeAuthorPic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Photo",
                table: "Author");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "86a52332-5e89-4671-aadf-e0d039391004", null, "Admin", "ADMIN" },
                    { "9c74be8b-a226-4f1e-b37f-e2b4daec7aad", null, "Manager", "MANAGER" },
                    { "d94cbf1a-33fa-4e95-ae11-52c6ccaa9e24", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86a52332-5e89-4671-aadf-e0d039391004");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c74be8b-a226-4f1e-b37f-e2b4daec7aad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d94cbf1a-33fa-4e95-ae11-52c6ccaa9e24");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Author",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
