using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 31, DateTimeKind.Local).AddTicks(2848),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(6077));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 32, DateTimeKind.Local).AddTicks(2965),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(9118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 31, DateTimeKind.Local).AddTicks(6010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(7176));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleDescription", "RoleName" },
                values: new object[,]
                {
                    { 1, "This role can manage all system", "admin" },
                    { 2, "This role can manage inventory, approve request", "manager" },
                    { 3, "This role just can view inventory, create request", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(6077),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 31, DateTimeKind.Local).AddTicks(2848));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(9118),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 32, DateTimeKind.Local).AddTicks(2965));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(7176),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 31, DateTimeKind.Local).AddTicks(6010));
        }
    }
}
