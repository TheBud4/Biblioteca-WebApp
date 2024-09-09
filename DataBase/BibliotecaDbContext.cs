using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.DataBase;

public class BibliotecaDbContext : DbContext {
    public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options) : base(options) {
    }

    public DbSet<Autor> Autores { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Emprestimo> Emprestimos { get; set; }
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // Configura o discriminador para a hierarquia de Usuario
        modelBuilder.Entity<Usuario>()
            .HasDiscriminator<string>("TipoUsuario")
            .HasValue<Funcionario>("Funcionario")
            .HasValue<Cliente>("Cliente");

        base.OnModelCreating(modelBuilder);
    }
}