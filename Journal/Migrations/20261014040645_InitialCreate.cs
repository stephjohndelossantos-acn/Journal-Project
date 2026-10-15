using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Journal.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JournalItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    JournalTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalItems_ProductTypes_JournalTypeId",
                        column: x => x.JournalTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "ImageUrl", "Name", "Section", "SortOrder", "Subtitle" },
                values: new object[,]
                {
                    { 1, "", "Leather Wrapped", 1, 1, "A5/A6 Size" },
                    { 2, "", "Feather Wrapped", 1, 2, "A5/A6 Size" },
                    { 3, "", "Crocodile Skin", 1, 3, "A4/A5" },
                    { 4, "", "Custom Wrapped", 1, 4, "Any Size" },
                    { 5, "", "Charms & Beads", 2, 1, "" },
                    { 6, "", "Ruled Notebook", 2, 2, "" },
                    { 7, "", "Pen & Pen Holder", 2, 3, "" },
                    { 8, "", "PVC Insert", 2, 4, "" }
                });

            migrationBuilder.InsertData(
                table: "JournalItems",
                columns: new[] { "Id", "Code", "Description", "ImageUrl", "IsFeatured", "JournalTypeId", "Price", "SortOrder", "Title" },
                values: new object[,]
                {
                    { 1, "leather01", "Hand-wrapped full-grain leather.", "", true, 1, 1500m, 1, "Classic Leather Journal" },
                    { 2, "leather02", "Refillable A6 cover.", "", false, 1, 1300m, 2, "Brown Leather Journal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalItems_JournalTypeId",
                table: "JournalItems",
                column: "JournalTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalItems");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
