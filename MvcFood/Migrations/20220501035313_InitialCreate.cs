using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcFood.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodTable",
                columns: table => new
                {
                    fdcId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTable", x => x.fdcId);
                });

            migrationBuilder.CreateTable(
                name: "NutrientTable",
                columns: table => new
                {
                    nutrientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nutrientName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutrientTable", x => x.nutrientId);
                });

            migrationBuilder.CreateTable(
                name: "Food_NutrientTable",
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
                    table.PrimaryKey("PK_Food_NutrientTable", x => x.FNId);
                    table.ForeignKey(
                        name: "FK_Food_NutrientTable_FoodTable_foodfdcId",
                        column: x => x.foodfdcId,
                        principalTable: "FoodTable",
                        principalColumn: "fdcId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Food_NutrientTable_NutrientTable_nutrientId",
                        column: x => x.nutrientId,
                        principalTable: "NutrientTable",
                        principalColumn: "nutrientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_NutrientTable_foodfdcId",
                table: "Food_NutrientTable",
                column: "foodfdcId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_NutrientTable_nutrientId",
                table: "Food_NutrientTable",
                column: "nutrientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food_NutrientTable");

            migrationBuilder.DropTable(
                name: "FoodTable");

            migrationBuilder.DropTable(
                name: "NutrientTable");
        }
    }
}
