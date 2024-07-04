using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initialdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(2836),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 16, 18, 709, DateTimeKind.Local).AddTicks(8475));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(7474),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 16, 18, 710, DateTimeKind.Local).AddTicks(5723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(8083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 16, 18, 710, DateTimeKind.Local).AddTicks(7355));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 16, 18, 709, DateTimeKind.Local).AddTicks(8475),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(2836));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 16, 18, 710, DateTimeKind.Local).AddTicks(5723),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(7474));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 16, 18, 710, DateTimeKind.Local).AddTicks(7355),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(8083));
        }
    }
}
