using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionType",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Employee" },
                    { 2, "Manager" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "EmployeeFirstName", "EmployeeLastName", "GrantedDate", "PermissionTypeId" },
                values: new object[,]
                {
                    { 1, "Adrian", "Arce", new DateTime(2023, 5, 27, 14, 15, 25, 336, DateTimeKind.Local).AddTicks(3587), 1 },
                    { 2, "Alejandro", "Diaz", new DateTime(2023, 5, 27, 14, 15, 25, 336, DateTimeKind.Local).AddTicks(3596), 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PermissionType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PermissionType",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
