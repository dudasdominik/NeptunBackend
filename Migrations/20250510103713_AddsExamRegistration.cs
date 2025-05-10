using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeptunBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddsExamRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentNeptunCode = table.Column<string>(type: "character varying(5)", nullable: false),
                    ExamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamRegistrations_Exams_ExamId",
                        column: x => x.ExamId,
                        principalTable: "Exams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamRegistrations_Students_StudentNeptunCode",
                        column: x => x.StudentNeptunCode,
                        principalTable: "Students",
                        principalColumn: "NeptunCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamRegistrations_ExamId",
                table: "ExamRegistrations",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRegistrations_StudentNeptunCode",
                table: "ExamRegistrations",
                column: "StudentNeptunCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamRegistrations");
        }
    }
}
