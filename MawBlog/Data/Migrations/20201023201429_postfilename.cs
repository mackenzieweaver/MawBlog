using Microsoft.EntityFrameworkCore.Migrations;

namespace MawBlog.Data.Migrations
{
    public partial class postfilename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchString",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Post",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Post");

            migrationBuilder.AddColumn<string>(
                name: "SearchString",
                table: "Post",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
