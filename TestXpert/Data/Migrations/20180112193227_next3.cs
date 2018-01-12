using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestXpert.Data.Migrations
{
    public partial class next3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_Answers",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_Answers",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Answers",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "Question",
                table: "Answers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Question",
                table: "Answers",
                column: "Question");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_Question",
                table: "Answers",
                column: "Question",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_Question",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_Question",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "Answers",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Answers",
                table: "Answers",
                column: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_Answers",
                table: "Answers",
                column: "Answers",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
