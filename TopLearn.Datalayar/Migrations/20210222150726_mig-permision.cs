using Microsoft.EntityFrameworkCore.Migrations;

namespace TopLearn.Datalayar.Migrations
{
    public partial class migpermision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permisions",
                columns: table => new
                {
                    PermisionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermisionTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisions", x => x.PermisionId);
                    table.ForeignKey(
                        name: "FK_Permisions_Permisions_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Permisions",
                        principalColumn: "PermisionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleToPermisions",
                columns: table => new
                {
                    RP_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermisionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleToPermisions", x => x.RP_Id);
                    table.ForeignKey(
                        name: "FK_RoleToPermisions_Permisions_PermisionId",
                        column: x => x.PermisionId,
                        principalTable: "Permisions",
                        principalColumn: "PermisionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleToPermisions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permisions_ParentId",
                table: "Permisions",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleToPermisions_PermisionId",
                table: "RoleToPermisions",
                column: "PermisionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleToPermisions_RoleId",
                table: "RoleToPermisions",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleToPermisions");

            migrationBuilder.DropTable(
                name: "Permisions");
        }
    }
}
