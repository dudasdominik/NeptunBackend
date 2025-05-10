using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeptunBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Universtiy",
                table: "Teachers",
                newName: "University");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "University",
                table: "Teachers",
                newName: "Universtiy");
        }
    }
}
