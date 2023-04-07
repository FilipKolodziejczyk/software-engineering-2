using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SoftwareEngineering2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SampleModelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleModelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SampleModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SampleModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SampleModels_SampleModelTypes_Id",
                        column: x => x.Id,
                        principalTable: "SampleModelTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SampleModelTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Type 1" },
                    { 2, "Type 2" },
                    { 3, "Type 3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SampleModels");

            migrationBuilder.DropTable(
                name: "SampleModelTypes");
        }
    }
}
