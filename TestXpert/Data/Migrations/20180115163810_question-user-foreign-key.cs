using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestXpert.Data.Migrations
{
    public partial class questionuserforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser",
                table: "Questions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ApplicationUser",
                table: "Questions",
                column: "ApplicationUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_ApplicationUser",
                table: "Questions",
                column: "ApplicationUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_ApplicationUser",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ApplicationUser",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ApplicationUser",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Questions",
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
    }
}
