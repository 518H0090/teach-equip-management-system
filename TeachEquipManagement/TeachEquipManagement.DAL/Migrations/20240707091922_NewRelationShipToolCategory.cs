using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeachEquipManagement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewRelationShipToolCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Categories_CategoryId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_CategoryId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "SubjectRelative",
                table: "Tools");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 153, DateTimeKind.Local).AddTicks(4114),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 346, DateTimeKind.Local).AddTicks(9716));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 151, DateTimeKind.Local).AddTicks(8112),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 344, DateTimeKind.Local).AddTicks(2724));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 153, DateTimeKind.Local).AddTicks(9536),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 347, DateTimeKind.Local).AddTicks(6926));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 152, DateTimeKind.Local).AddTicks(7171),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 345, DateTimeKind.Local).AddTicks(7503));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 152, DateTimeKind.Local).AddTicks(8133),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 345, DateTimeKind.Local).AddTicks(9403));

            migrationBuilder.CreateTable(
                name: "ToolCategories",
                columns: table => new
                {
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolCategories", x => new { x.ToolId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ToolCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToolCategories_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToolCategories_CategoryId",
                table: "ToolCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToolCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Tools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SubjectRelative",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 346, DateTimeKind.Local).AddTicks(9716),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 153, DateTimeKind.Local).AddTicks(4114));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InvoiceDate",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 344, DateTimeKind.Local).AddTicks(2724),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 151, DateTimeKind.Local).AddTicks(8112));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ActionDate",
                table: "InventoryHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 347, DateTimeKind.Local).AddTicks(6926),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 153, DateTimeKind.Local).AddTicks(9536));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RequestDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 345, DateTimeKind.Local).AddTicks(7503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 152, DateTimeKind.Local).AddTicks(7171));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ApproveDate",
                table: "ApprovalRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 7, 15, 44, 9, 345, DateTimeKind.Local).AddTicks(9403),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 7, 7, 16, 19, 22, 152, DateTimeKind.Local).AddTicks(8133));

            migrationBuilder.CreateIndex(
                name: "IX_Tools_CategoryId",
                table: "Tools",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Categories_CategoryId",
                table: "Tools",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
