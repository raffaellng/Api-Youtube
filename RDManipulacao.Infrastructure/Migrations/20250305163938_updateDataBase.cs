using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RDManipulacao.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Autor",
                table: "Videos",
                newName: "Author");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Videos",
                newName: "Autor");
        }
    }
}
