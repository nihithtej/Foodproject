using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFood.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    fdcId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.fdcId);
                });

            migrationBuilder.CreateTable(
                name: "Nutrient",
                columns: table => new
                {
                    nutrientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nutrientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nutrientNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrient", x => x.nutrientId);
                });

            migrationBuilder.CreateTable(
                name: "Food_Nutrient",
                columns: table => new
                {
                    FNId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<float>(type: "real", nullable: false),
                    unitName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    foodfdcId = table.Column<int>(type: "int", nullable: true),
                    nutrientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food_Nutrient", x => x.FNId);
                    table.ForeignKey(
                        name: "FK_Food_Nutrient_Food_foodfdcId",
                        column: x => x.foodfdcId,
                        principalTable: "Food",
                        principalColumn: "fdcId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Food_Nutrient_Nutrient_nutrientId",
                        column: x => x.nutrientId,
                        principalTable: "Nutrient",
                        principalColumn: "nutrientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_Nutrient_foodfdcId",
                table: "Food_Nutrient",
                column: "foodfdcId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_Nutrient_nutrientId",
                table: "Food_Nutrient",
                column: "nutrientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food_Nutrient");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropTable(
                name: "Nutrient");
        }
    }
}
