using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class newdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Tool_ToolId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Tool_ToolId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Tool_Categories_CategoryId",
                table: "Tool");

            migrationBuilder.DropForeignKey(
                name: "FK_Tool_Suppliers_SupplierId",
                table: "Tool");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RefreshToken_RefreshTokenId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tool",
                table: "Tool");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "Tool",
                newName: "Tools");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                newName: "RefreshTokens");

            migrationBuilder.RenameIndex(
                name: "IX_Tool_SupplierId",
                table: "Tools",
                newName: "IX_Tools_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Tool_CategoryId",
                table: "Tools",
                newName: "IX_Tools_CategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 39, 49, 287, DateTimeKind.Local).AddTicks(4417),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(2836));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 39, 49, 287, DateTimeKind.Local).AddTicks(9960),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(7474));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 39, 49, 288, DateTimeKind.Local).AddTicks(621),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(8083));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 39, 49, 288, DateTimeKind.Local).AddTicks(4594),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tools",
                table: "Tools",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InventoryHistories",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 7, 4, 22, 39, 49, 288, DateTimeKind.Local).AddTicks(8264)),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryHistories", x => new { x.UserId, x.InventoryId });
                    table.ForeignKey(
                        name: "FK_InventoryHistories_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryHistories_InventoryId",
                table: "InventoryHistories",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Tools_ToolId",
                table: "Inventories",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Tools_ToolId",
                table: "Invoices",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Categories_CategoryId",
                table: "Tools",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Suppliers_SupplierId",
                table: "Tools",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RefreshTokens_RefreshTokenId",
                table: "Users",
                column: "RefreshTokenId",
                principalTable: "RefreshTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Tools_ToolId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Tools_ToolId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Categories_CategoryId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Suppliers_SupplierId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_RefreshTokens_RefreshTokenId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "InventoryHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tools",
                table: "Tools");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "Tools",
                newName: "Tool");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "RefreshToken");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_SupplierId",
                table: "Tool",
                newName: "IX_Tool_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_CategoryId",
                table: "Tool",
                newName: "IX_Tool_CategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(2836),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 39, 49, 287, DateTimeKind.Local).AddTicks(4417));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(7474),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 39, 49, 287, DateTimeKind.Local).AddTicks(9960));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 4, 22, 20, 34, 76, DateTimeKind.Local).AddTicks(8083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 39, 49, 288, DateTimeKind.Local).AddTicks(621));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "RefreshToken",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 4, 22, 39, 49, 288, DateTimeKind.Local).AddTicks(4594));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tool",
                table: "Tool",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Tool_ToolId",
                table: "Inventories",
                column: "ToolId",
                principalTable: "Tool",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Tool_ToolId",
                table: "Invoices",
                column: "ToolId",
                principalTable: "Tool",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tool_Categories_CategoryId",
                table: "Tool",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tool_Suppliers_SupplierId",
                table: "Tool",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RefreshToken_RefreshTokenId",
                table: "Users",
                column: "RefreshTokenId",
                principalTable: "RefreshToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
