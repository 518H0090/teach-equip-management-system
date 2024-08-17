using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnitFromCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Categories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 9, 7, 16, 828, DateTimeKind.Local).AddTicks(2931),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 15, 15, 32, 10, 87, DateTimeKind.Local).AddTicks(5158));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 9, 7, 16, 830, DateTimeKind.Local).AddTicks(3035),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 15, 15, 32, 10, 89, DateTimeKind.Local).AddTicks(5846));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 17, 9, 7, 16, 829, DateTimeKind.Local).AddTicks(519),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 15, 15, 32, 10, 88, DateTimeKind.Local).AddTicks(2003));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 15, 15, 32, 10, 87, DateTimeKind.Local).AddTicks(5158),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 9, 7, 16, 828, DateTimeKind.Local).AddTicks(2931));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 15, 15, 32, 10, 89, DateTimeKind.Local).AddTicks(5846),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 9, 7, 16, 830, DateTimeKind.Local).AddTicks(3035));

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 15, 15, 32, 10, 88, DateTimeKind.Local).AddTicks(2003),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 17, 9, 7, 16, 829, DateTimeKind.Local).AddTicks(519));
        }
    }
}
