using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeptunBackend.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherNeptunCode",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseStudent");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherNeptunCode",
                table: "Courses",
                type: "character varying(5)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(5)");

            migrationBuilder.AddColumn<string>(
                name: "StudentNeptunCode",
                table: "Courses",
                type: "character varying(5)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StudentNeptunCode",
                table: "Courses",
                column: "StudentNeptunCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Students_StudentNeptunCode",
                table: "Courses",
                column: "StudentNeptunCode",
                principalTable: "Students",
                principalColumn: "NeptunCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeacherNeptunCode",
                table: "Courses",
                column: "TeacherNeptunCode",
                principalTable: "Teachers",
                principalColumn: "NeptunCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Students_StudentNeptunCode",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherNeptunCode",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StudentNeptunCode",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "StudentNeptunCode",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "TeacherNeptunCode",
                table: "Courses",
                type: "character varying(5)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CourseStudent",
                columns: table => new
                {
                    CoursesId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentsNeptunCode = table.Column<string>(type: "character varying(5)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseStudent", x => new { x.CoursesId, x.StudentsNeptunCode });
                    table.ForeignKey(
                        name: "FK_CourseStudent_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseStudent_Students_StudentsNeptunCode",
                        column: x => x.StudentsNeptunCode,
                        principalTable: "Students",
                        principalColumn: "NeptunCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseStudent_StudentsNeptunCode",
                table: "CourseStudent",
                column: "StudentsNeptunCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_TeacherNeptunCode",
                table: "Courses",
                column: "TeacherNeptunCode",
                principalTable: "Teachers",
                principalColumn: "NeptunCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
