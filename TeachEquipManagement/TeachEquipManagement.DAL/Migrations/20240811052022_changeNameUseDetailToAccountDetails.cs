using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeNameUseDetailToAccountDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_Accounts_AccountId",
                table: "UserDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails");

            migrationBuilder.RenameTable(
                name: "UserDetails",
                newName: "AccountDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 12, 20, 21, 144, DateTimeKind.Local).AddTicks(8136),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 31, DateTimeKind.Local).AddTicks(2848));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 12, 20, 21, 145, DateTimeKind.Local).AddTicks(8508),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 32, DateTimeKind.Local).AddTicks(2965));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 11, 12, 20, 21, 145, DateTimeKind.Local).AddTicks(1404),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 31, DateTimeKind.Local).AddTicks(6010));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountDetails",
                table: "AccountDetails",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountDetails_Accounts_AccountId",
                table: "AccountDetails",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountDetails_Accounts_AccountId",
                table: "AccountDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountDetails",
                table: "AccountDetails");

            migrationBuilder.RenameTable(
                name: "AccountDetails",
                newName: "UserDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 31, DateTimeKind.Local).AddTicks(2848),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 12, 20, 21, 144, DateTimeKind.Local).AddTicks(8136));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 32, DateTimeKind.Local).AddTicks(2965),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 12, 20, 21, 145, DateTimeKind.Local).AddTicks(8508));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 8, 7, 9, 19, 49, 31, DateTimeKind.Local).AddTicks(6010),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 8, 11, 12, 20, 21, 145, DateTimeKind.Local).AddTicks(1404));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDetails",
                table: "UserDetails",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_Accounts_AccountId",
                table: "UserDetails",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
