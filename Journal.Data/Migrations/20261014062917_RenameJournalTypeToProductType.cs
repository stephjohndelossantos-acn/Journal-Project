using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Journal.Migrations
{
    /// <inheritdoc />
    public partial class RenameJournalTypeToProductType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalItems_ProductTypes_JournalTypeId",
                table: "JournalItems");

            migrationBuilder.RenameColumn(
                name: "JournalTypeId",
                table: "JournalItems",
                newName: "ProductTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_JournalItems_JournalTypeId",
                table: "JournalItems",
                newName: "IX_JournalItems_ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalItems_ProductTypes_ProductTypeId",
                table: "JournalItems",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalItems_ProductTypes_ProductTypeId",
                table: "JournalItems");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "JournalItems",
                newName: "JournalTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_JournalItems_ProductTypeId",
                table: "JournalItems",
                newName: "IX_JournalItems_JournalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalItems_ProductTypes_JournalTypeId",
                table: "JournalItems",
                column: "JournalTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
