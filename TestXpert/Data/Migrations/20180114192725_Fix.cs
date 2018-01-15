using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestXpert.Data.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Questions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ApplicationUserId",
                table: "Questions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_ApplicationUserId",
                table: "Questions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_ApplicationUserId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ApplicationUserId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Questions");
        }
    }
}
