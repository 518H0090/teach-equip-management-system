using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeToAccountIdForInventoryHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHistories_Accounts_UserId",
                table: "InventoryHistories");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "InventoryHistories",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryHistories_UserId",
                table: "InventoryHistories",
                newName: "IX_InventoryHistories_AccountId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(6077),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 16, 40, 46, 795, DateTimeKind.Local).AddTicks(604));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(9118),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 16, 40, 46, 795, DateTimeKind.Local).AddTicks(4046));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(7176),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 29, 16, 40, 46, 795, DateTimeKind.Local).AddTicks(1857));

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHistories_Accounts_AccountId",
                table: "InventoryHistories",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryHistories_Accounts_AccountId",
                table: "InventoryHistories");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "InventoryHistories",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InventoryHistories_AccountId",
                table: "InventoryHistories",
                newName: "IX_InventoryHistories_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 16, 40, 46, 795, DateTimeKind.Local).AddTicks(604),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(6077));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 16, 40, 46, 795, DateTimeKind.Local).AddTicks(4046),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(9118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 29, 16, 40, 46, 795, DateTimeKind.Local).AddTicks(1857),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 30, 9, 47, 40, 638, DateTimeKind.Local).AddTicks(7176));

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryHistories_Accounts_UserId",
                table: "InventoryHistories",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
