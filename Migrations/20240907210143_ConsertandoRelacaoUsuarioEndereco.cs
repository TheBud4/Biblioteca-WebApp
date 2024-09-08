using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class ConsertandoRelacaoUsuarioEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Livros_LivroId",
                table: "Emprestimos");

            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioId",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Autores_AutorId",
                table: "Livros");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Categorias_CategoryId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Livros_CategoryId",
                table: "Livros");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_UsuarioId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Enderecos");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Usuarios",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Usuarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Livros",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EnderecoId",
                table: "Usuarios",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_CategoriaId",
                table: "Livros",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Livros_LivroId",
                table: "Emprestimos",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Autores_AutorId",
                table: "Livros",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Categorias_CategoriaId",
                table: "Livros",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Enderecos_EnderecoId",
                table: "Usuarios",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emprestimos_Livros_LivroId",
                table: "Emprestimos");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Autores_AutorId",
                table: "Livros");

            migrationBuilder.DropForeignKey(
                name: "FK_Livros_Categorias_CategoriaId",
                table: "Livros");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Enderecos_EnderecoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_EnderecoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Livros_CategoriaId",
                table: "Livros");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Livros");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Enderecos",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Livros_CategoryId",
                table: "Livros",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_UsuarioId",
                table: "Enderecos",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Emprestimos_Livros_LivroId",
                table: "Emprestimos",
                column: "LivroId",
                principalTable: "Livros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Usuarios_UsuarioId",
                table: "Enderecos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Autores_AutorId",
                table: "Livros",
                column: "AutorId",
                principalTable: "Autores",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Livros_Categorias_CategoryId",
                table: "Livros",
                column: "CategoryId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
