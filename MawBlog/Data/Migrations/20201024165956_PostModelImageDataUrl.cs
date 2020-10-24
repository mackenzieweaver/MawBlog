using Microsoft.EntityFrameworkCore.Migrations;

namespace MawBlog.Data.Migrations
{
    public partial class PostModelImageDataUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageDataUrl",
                table: "Post",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageDataUrl",
                table: "Post");
        }
    }
}
