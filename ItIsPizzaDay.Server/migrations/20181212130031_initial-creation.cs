using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ItIsPizzaDay.Server.Migrations
{
    public partial class initialcreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ingredient",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    price = table.Column<decimal>(type: "numeric(4,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ingredient", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "muppet",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    real_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_muppet", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "type",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date = table.Column<DateTime>(nullable: true),
                    muppet = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "order_muppet_fkey",
                        column: x => x.muppet,
                        principalTable: "muppet",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "food",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    price = table.Column<decimal>(type: "numeric(4,2)", nullable: false),
                    visible = table.Column<bool>(nullable: false),
                    type = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food", x => x.id);
                    table.ForeignKey(
                        name: "food_type_fkey",
                        column: x => x.type,
                        principalTable: "type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "food_ingredient",
                columns: table => new
                {
                    food = table.Column<Guid>(nullable: false),
                    ingredient = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_ingredient", x => new { x.food, x.ingredient });
                    table.ForeignKey(
                        name: "food_ingredient_food_id_fk",
                        column: x => x.food,
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "food_ingredient_ingredient_id_fk",
                        column: x => x.ingredient,
                        principalTable: "ingredient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "food_order",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    food = table.Column<Guid>(nullable: true),
                    order = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_order", x => x.id);
                    table.ForeignKey(
                        name: "food_order_food_fkey",
                        column: x => x.food,
                        principalTable: "food",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "food_order_order_fkey",
                        column: x => x.order,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "food_order_ingredient",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    food_order = table.Column<Guid>(nullable: false),
                    ingredient = table.Column<Guid>(nullable: false),
                    isremoval = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_food_order_ingredient", x => x.id);
                    table.ForeignKey(
                        name: "food_order_ingredient_food_order_fkey",
                        column: x => x.food_order,
                        principalTable: "food_order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "food_order_ingredient_ingredient_fkey",
                        column: x => x.ingredient,
                        principalTable: "ingredient",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_food_type",
                table: "food",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_food_ingredient_ingredient",
                table: "food_ingredient",
                column: "ingredient");

            migrationBuilder.CreateIndex(
                name: "IX_food_order_food",
                table: "food_order",
                column: "food");

            migrationBuilder.CreateIndex(
                name: "IX_food_order_order",
                table: "food_order",
                column: "order");

            migrationBuilder.CreateIndex(
                name: "IX_food_order_ingredient_food_order",
                table: "food_order_ingredient",
                column: "food_order");

            migrationBuilder.CreateIndex(
                name: "IX_food_order_ingredient_ingredient",
                table: "food_order_ingredient",
                column: "ingredient");

            migrationBuilder.CreateIndex(
                name: "IX_order_muppet",
                table: "order",
                column: "muppet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "food_ingredient");

            migrationBuilder.DropTable(
                name: "food_order_ingredient");

            migrationBuilder.DropTable(
                name: "food_order");

            migrationBuilder.DropTable(
                name: "ingredient");

            migrationBuilder.DropTable(
                name: "food");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "type");

            migrationBuilder.DropTable(
                name: "muppet");
        }
    }
}
