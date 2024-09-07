using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoDiscriminador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Usuarios",
                newName: "TipoUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoUsuario",
                table: "Usuarios",
                newName: "Discriminator");
        }
    }
}
