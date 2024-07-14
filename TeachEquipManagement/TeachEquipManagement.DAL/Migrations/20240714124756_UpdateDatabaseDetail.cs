using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPermissions");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "SpoFileId",
                table: "UserDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 321, DateTimeKind.Local).AddTicks(5497),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 14, 2, 36, 885, DateTimeKind.Local).AddTicks(1600));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 324, DateTimeKind.Local).AddTicks(830),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 14, 2, 36, 886, DateTimeKind.Local).AddTicks(8193));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 322, DateTimeKind.Local).AddTicks(5476),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 14, 2, 36, 885, DateTimeKind.Local).AddTicks(9182));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 322, DateTimeKind.Local).AddTicks(7360),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 10, 14, 2, 36, 885, DateTimeKind.Local).AddTicks(9860));

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpoFileId",
                table: "UserDetails");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 14, 2, 36, 885, DateTimeKind.Local).AddTicks(1600),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 321, DateTimeKind.Local).AddTicks(5497));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 14, 2, 36, 886, DateTimeKind.Local).AddTicks(8193),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 324, DateTimeKind.Local).AddTicks(830));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 14, 2, 36, 885, DateTimeKind.Local).AddTicks(9182),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 322, DateTimeKind.Local).AddTicks(5476));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 14, 2, 36, 885, DateTimeKind.Local).AddTicks(9860),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 14, 19, 47, 54, 322, DateTimeKind.Local).AddTicks(7360));

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPermissions",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_UserPermissions_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPermissions_PermissionId",
                table: "UserPermissions",
                column: "PermissionId");
        }
    }
}
