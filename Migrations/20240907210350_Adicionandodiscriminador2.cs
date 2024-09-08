using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class Adicionandodiscriminador2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Usuarios",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}
