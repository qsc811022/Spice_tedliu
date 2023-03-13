using Microsoft.EntityFrameworkCore.Migrations;

namespace Spice_tedliu.Data.Migrations
{
    public partial class addSubCategoryToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SubCategroy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategroy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategroy_Categroy_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categroy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCategroy_CategoryId",
                table: "SubCategroy",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubCategroy");
        }
    }
}
