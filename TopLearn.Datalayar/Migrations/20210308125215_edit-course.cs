using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.Datalayar.Migrations
{
    public partial class editcourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseLevels_CourseLevelLevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatus_CourseStatusStatusId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseLevelLevelId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseStatusStatusId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseLevelLevelId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseStatusStatusId",
                table: "Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_LevelId",
                table: "Courses",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StatusId",
                table: "Courses",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseLevels_LevelId",
                table: "Courses",
                column: "LevelId",
                principalTable: "CourseLevels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatus_StatusId",
                table: "Courses",
                column: "StatusId",
                principalTable: "CourseStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseLevels_LevelId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatus_StatusId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_LevelId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StatusId",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CourseLevelLevelId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseStatusStatusId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseLevelLevelId",
                table: "Courses",
                column: "CourseLevelLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseStatusStatusId",
                table: "Courses",
                column: "CourseStatusStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseLevels_CourseLevelLevelId",
                table: "Courses",
                column: "CourseLevelLevelId",
                principalTable: "CourseLevels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatus_CourseStatusStatusId",
                table: "Courses",
                column: "CourseStatusStatusId",
                principalTable: "CourseStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
