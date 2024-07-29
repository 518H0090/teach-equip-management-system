using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseForInventoryHistoryAndApprovalRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryHistories",
                table: "InventoryHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApprovalRequests",
                table: "ApprovalRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 15, 0, 24, 13, DateTimeKind.Local).AddTicks(7428),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 23, 20, 55, 23, 542, DateTimeKind.Local).AddTicks(7886));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 15, 0, 24, 14, DateTimeKind.Local).AddTicks(798),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 23, 20, 55, 23, 544, DateTimeKind.Local).AddTicks(4829));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InventoryHistories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 15, 0, 24, 13, DateTimeKind.Local).AddTicks(8658),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 23, 20, 55, 23, 543, DateTimeKind.Local).AddTicks(5128));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ApprovalRequests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryHistories",
                table: "InventoryHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApprovalRequests",
                table: "ApprovalRequests",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistories_UserId",
                table: "InventoryHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApprovalRequests_AccountId",
                table: "ApprovalRequests",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InventoryHistories",
                table: "InventoryHistories");

            migrationBuilder.DropIndex(
                name: "IX_InventoryHistories_UserId",
                table: "InventoryHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApprovalRequests",
                table: "ApprovalRequests");

            migrationBuilder.DropIndex(
                name: "IX_ApprovalRequests_AccountId",
                table: "ApprovalRequests");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InventoryHistories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ApprovalRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 23, 20, 55, 23, 542, DateTimeKind.Local).AddTicks(7886),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 15, 0, 24, 13, DateTimeKind.Local).AddTicks(7428));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 23, 20, 55, 23, 544, DateTimeKind.Local).AddTicks(4829),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 15, 0, 24, 14, DateTimeKind.Local).AddTicks(798));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 23, 20, 55, 23, 543, DateTimeKind.Local).AddTicks(5128),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 15, 0, 24, 13, DateTimeKind.Local).AddTicks(8658));

            migrationBuilder.AddPrimaryKey(
                name: "PK_InventoryHistories",
                table: "InventoryHistories",
                columns: new[] { "UserId", "InventoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApprovalRequests",
                table: "ApprovalRequests",
                columns: new[] { "AccountId", "InventoryId" });
        }
    }
}
