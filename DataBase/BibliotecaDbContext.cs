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
        
        // Relacionamento 1:N entre Autor e Livro
        modelBuilder.Entity<Livro>()
            .HasOne(l => l.Autor)
            .WithMany(a => a.Livros)
            .HasForeignKey(l => l.AutorId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relacionamento 1:N entre Categoria e Livro
        modelBuilder.Entity<Livro>()
            .HasOne(l => l.Categoria)
            .WithMany(c => c.Livros)
            .HasForeignKey(l => l.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        // Relacionamento 1:N entre Cliente e Emprestimo
        modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.Cliente)
            .WithMany(c => c.Emprestimos)
            .HasForeignKey(e => e.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento 1:N entre Livro e Emprestimo
        modelBuilder.Entity<Emprestimo>()
            .HasOne(e => e.Livro)
            .WithMany()
            .HasForeignKey(e => e.LivroId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento 1:1 entre Usuario e Endereco
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Endereco)
            .WithOne(e => e.Usuario)
            .HasForeignKey<Endereco>(e => e.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}