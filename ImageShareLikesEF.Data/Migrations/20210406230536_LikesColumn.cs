using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageShareLikesEF.Data.Migrations
{
    public partial class LikesColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Views",
                table: "Images",
                newName: "Likes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Likes",
                table: "Images",
                newName: "Views");
        }
    }
}
