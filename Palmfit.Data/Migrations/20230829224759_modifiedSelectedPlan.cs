using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Palmfit.Data.Migrations
{
    /// <inheritdoc />
    public partial class modifiedSelectedPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedPlans_MealPlans_MealPlanId",
                table: "SelectedPlans");

            migrationBuilder.RenameColumn(
                name: "MealPlanId",
                table: "SelectedPlans",
                newName: "FoodClassId");

            migrationBuilder.RenameIndex(
                name: "IX_SelectedPlans_MealPlanId",
                table: "SelectedPlans",
                newName: "IX_SelectedPlans_FoodClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedPlans_FoodClasses_FoodClassId",
                table: "SelectedPlans",
                column: "FoodClassId",
                principalTable: "FoodClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedPlans_FoodClasses_FoodClassId",
                table: "SelectedPlans");

            migrationBuilder.RenameColumn(
                name: "FoodClassId",
                table: "SelectedPlans",
                newName: "MealPlanId");

            migrationBuilder.RenameIndex(
                name: "IX_SelectedPlans_FoodClassId",
                table: "SelectedPlans",
                newName: "IX_SelectedPlans_MealPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedPlans_MealPlans_MealPlanId",
                table: "SelectedPlans",
                column: "MealPlanId",
                principalTable: "MealPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
