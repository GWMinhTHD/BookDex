using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebStore.Server.Migrations
{
    /// <inheritdoc />
    public partial class addDateToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c0f1b03-426d-49d0-98e3-d82d7c325dc7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e45d2fe-8819-4597-bbc4-5303982810b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9e5c01b-36a7-4f61-9273-bff679ed4135");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Order",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Order");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5c0f1b03-426d-49d0-98e3-d82d7c325dc7", null, "Admin", "ADMIN" },
                    { "5e45d2fe-8819-4597-bbc4-5303982810b3", null, "Customer", "CUSTOMER" },
                    { "f9e5c01b-36a7-4f61-9273-bff679ed4135", null, "Manager", "MANAGER" }
                });
        }
    }
}
