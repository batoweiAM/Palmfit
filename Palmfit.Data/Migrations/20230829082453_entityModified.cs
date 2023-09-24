using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Palmfit.Data.Migrations
{
    /// <inheritdoc />
    public partial class entityModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealPlans_AppUser_AppUserId",
                table: "MealPlans");

            migrationBuilder.DropColumn(
                name: "DayOfTheWeek",
                table: "MealPlans");

            migrationBuilder.RenameColumn(
                name: "Week",
                table: "MealPlans",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "MealPlans",
                newName: "foodClassId");

            migrationBuilder.RenameIndex(
                name: "IX_MealPlans_AppUserId",
                table: "MealPlans",
                newName: "IX_MealPlans_foodClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlans_FoodClasses_foodClassId",
                table: "MealPlans",
                column: "foodClassId",
                principalTable: "FoodClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealPlans_FoodClasses_foodClassId",
                table: "MealPlans");

            migrationBuilder.RenameColumn(
                name: "foodClassId",
                table: "MealPlans",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "MealPlans",
                newName: "Week");

            migrationBuilder.RenameIndex(
                name: "IX_MealPlans_foodClassId",
                table: "MealPlans",
                newName: "IX_MealPlans_AppUserId");

            migrationBuilder.AddColumn<int>(
                name: "DayOfTheWeek",
                table: "MealPlans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlans_AppUser_AppUserId",
                table: "MealPlans",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
