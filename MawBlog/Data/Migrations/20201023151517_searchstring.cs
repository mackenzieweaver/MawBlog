﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MawBlog.Data.Migrations
{
    public partial class searchstring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchString",
                table: "Post",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchString",
                table: "Post");
        }
    }
}