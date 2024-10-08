﻿// <auto-generated />
using System;
using Biblioteca.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BibliotecaWebApp.Migrations
{
    [DbContext(typeof(BibliotecaDbContext))]
    [Migration("20240906205805_AdicionandoTabelaTipo")]
    partial class AdicionandoTabelaTipo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("Biblioteca.Models.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Autores");
                });

            modelBuilder.Entity("Biblioteca.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Biblioteca.Models.Emprestimo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataEmprestimo")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DataRetorno")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("FuncionarioId")
                        .HasColumnType("TEXT");

                    b.Property<int>("LivroId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("FuncionarioId");

                    b.HasIndex("LivroId");

                    b.ToTable("Emprestimos");
                });

            modelBuilder.Entity("Biblioteca.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rua")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Biblioteca.Models.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AutorId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("Biblioteca.Models.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(96)
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TipoUsuario")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasDiscriminator<string>("TipoUsuario").HasValue("Usuario");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Biblioteca.Models.Cliente", b =>
                {
                    b.HasBaseType("Biblioteca.Models.Usuario");

                    b.HasDiscriminator().HasValue("Cliente");
                });

            modelBuilder.Entity("Biblioteca.Models.Funcionario", b =>
                {
                    b.HasBaseType("Biblioteca.Models.Usuario");

                    b.Property<float>("Salario")
                        .HasColumnType("REAL");

                    b.HasDiscriminator().HasValue("Funcionario");
                });

            modelBuilder.Entity("Biblioteca.Models.Emprestimo", b =>
                {
                    b.HasOne("Biblioteca.Models.Cliente", "Cliente")
                        .WithMany("Emprestimos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Biblioteca.Models.Funcionario", null)
                        .WithMany("Emprestimos")
                        .HasForeignKey("FuncionarioId");

                    b.HasOne("Biblioteca.Models.Livro", "Livro")
                        .WithMany()
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("Biblioteca.Models.Endereco", b =>
                {
                    b.HasOne("Biblioteca.Models.Usuario", "Usuario")
                        .WithOne("Endereco")
                        .HasForeignKey("Biblioteca.Models.Endereco", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Biblioteca.Models.Livro", b =>
                {
                    b.HasOne("Biblioteca.Models.Autor", "Autor")
                        .WithMany("Livros")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Biblioteca.Models.Categoria", "Categoria")
                        .WithMany("Livros")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Autor");

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("Biblioteca.Models.Autor", b =>
                {
                    b.Navigation("Livros");
                });

            modelBuilder.Entity("Biblioteca.Models.Categoria", b =>
                {
                    b.Navigation("Livros");
                });

            modelBuilder.Entity("Biblioteca.Models.Usuario", b =>
                {
                    b.Navigation("Endereco")
                        .IsRequired();
                });

            modelBuilder.Entity("Biblioteca.Models.Cliente", b =>
                {
                    b.Navigation("Emprestimos");
                });

            modelBuilder.Entity("Biblioteca.Models.Funcionario", b =>
                {
                    b.Navigation("Emprestimos");
                });
#pragma warning restore 612, 618
        }
    }
}
