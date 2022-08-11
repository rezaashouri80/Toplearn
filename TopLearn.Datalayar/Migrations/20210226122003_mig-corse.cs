using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.Datalayar.Migrations
{
    public partial class migcorse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupCourses",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupCourses", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_GroupCourses_GroupCourses_ParentId",
                        column: x => x.ParentId,
                        principalTable: "GroupCourses",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupCourses_ParentId",
                table: "GroupCourses",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupCourses");
        }
    }
}
