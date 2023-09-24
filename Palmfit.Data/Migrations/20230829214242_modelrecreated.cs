using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Palmfit.Data.Migrations
{
    /// <inheritdoc />
    public partial class modelrecreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_FoodClasses_FoodClassId",
                table: "Foods");

            migrationBuilder.DropForeignKey(
                name: "FK_MealPlans_FoodClasses_foodClassId",
                table: "MealPlans");

            migrationBuilder.DropIndex(
                name: "IX_Foods_FoodClassId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "FoodClassId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "FoodClasses");

            migrationBuilder.RenameColumn(
                name: "foodClassId",
                table: "MealPlans",
                newName: "FoodClassId");

            migrationBuilder.RenameIndex(
                name: "IX_MealPlans_foodClassId",
                table: "MealPlans",
                newName: "IX_MealPlans_FoodClassId");

            migrationBuilder.RenameColumn(
                name: "Details",
                table: "FoodClasses",
                newName: "Discription");

            migrationBuilder.CreateTable(
                name: "SelectedPlans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    MealPlanId = table.Column<string>(type: "text", nullable: false),
                    AppUserId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelectedPlans_AppUser_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SelectedPlans_MealPlans_MealPlanId",
                        column: x => x.MealPlanId,
                        principalTable: "MealPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SelectedPlans_AppUserId",
                table: "SelectedPlans",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedPlans_MealPlanId",
                table: "SelectedPlans",
                column: "MealPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlans_FoodClasses_FoodClassId",
                table: "MealPlans",
                column: "FoodClassId",
                principalTable: "FoodClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealPlans_FoodClasses_FoodClassId",
                table: "MealPlans");

            migrationBuilder.DropTable(
                name: "SelectedPlans");

            migrationBuilder.RenameColumn(
                name: "FoodClassId",
                table: "MealPlans",
                newName: "foodClassId");

            migrationBuilder.RenameIndex(
                name: "IX_MealPlans_FoodClassId",
                table: "MealPlans",
                newName: "IX_MealPlans_foodClassId");

            migrationBuilder.RenameColumn(
                name: "Discription",
                table: "FoodClasses",
                newName: "Details");

            migrationBuilder.AddColumn<string>(
                name: "FoodClassId",
                table: "Foods",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FoodClasses",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddForeignKey(
                name: "FK_MealPlans_FoodClasses_foodClassId",
                table: "MealPlans",
                column: "foodClassId",
                principalTable: "FoodClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
