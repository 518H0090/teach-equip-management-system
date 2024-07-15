using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEntityApprovalRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Accounts_UserId",
                table: "ApprovalRequests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ApprovalRequests",
                newName: "AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "SpoFileId",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "UserDetails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 15, 11, 9, 7, 152, DateTimeKind.Local).AddTicks(3085),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 321, DateTimeKind.Local).AddTicks(5497));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 15, 11, 9, 7, 152, DateTimeKind.Local).AddTicks(8660),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 324, DateTimeKind.Local).AddTicks(830));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 15, 11, 9, 7, 152, DateTimeKind.Local).AddTicks(5323),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 322, DateTimeKind.Local).AddTicks(5476));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 15, 11, 9, 7, 152, DateTimeKind.Local).AddTicks(5600),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 322, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Accounts_AccountId",
                table: "ApprovalRequests",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApprovalRequests_Accounts_AccountId",
                table: "ApprovalRequests");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "ApprovalRequests",
                newName: "UserId");

            migrationBuilder.AlterColumn<string>(
                name: "SpoFileId",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "UserDetails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 321, DateTimeKind.Local).AddTicks(5497),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 15, 11, 9, 7, 152, DateTimeKind.Local).AddTicks(3085));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 324, DateTimeKind.Local).AddTicks(830),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 15, 11, 9, 7, 152, DateTimeKind.Local).AddTicks(8660));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 322, DateTimeKind.Local).AddTicks(5476),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 15, 11, 9, 7, 152, DateTimeKind.Local).AddTicks(5323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 322, DateTimeKind.Local).AddTicks(7360),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 15, 11, 9, 7, 152, DateTimeKind.Local).AddTicks(5600));

            migrationBuilder.AddForeignKey(
                name: "FK_ApprovalRequests_Accounts_UserId",
                table: "ApprovalRequests",
                column: "UserId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
