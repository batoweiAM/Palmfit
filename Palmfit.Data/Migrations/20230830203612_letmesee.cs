using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Palmfit.Data.Migrations
{
    /// <inheritdoc />
    public partial class letmesee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Foods_FoodClassId",
                table: "Foods",
                column: "FoodClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_FoodClasses_FoodClassId",
                table: "Foods",
                column: "FoodClassId",
                principalTable: "FoodClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodClasses_FoodClassId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodClassId",
                table: "Foods");
        }
    }
}
