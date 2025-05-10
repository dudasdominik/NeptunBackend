using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeptunBackend.Migrations
{
    /// <inheritdoc />
    public partial class OneToManyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_TeacherNeptunCode",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "TeacherNeptunCode",
                table: "Courses",
                newName: "NeptunCode");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_TeacherNeptunCode",
                table: "Courses",
                newName: "IX_Courses_NeptunCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Teachers_NeptunCode",
                table: "Courses",
                column: "NeptunCode",
                principalTable: "Teachers",
                principalColumn: "NeptunCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Teachers_NeptunCode",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "NeptunCode",
                table: "Courses",
                newName: "TeacherNeptunCode");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_NeptunCode",
                table: "Courses",
                newName: "IX_Courses_TeacherNeptunCode");

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
