using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class BancoRefeito2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Autores",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Autores",
                newName: "Name");
        }
    }
}
