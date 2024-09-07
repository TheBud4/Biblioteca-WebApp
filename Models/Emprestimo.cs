namespace Biblioteca.Models;

public class Emprestimo {
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime DataEmprestimo { get; set; } = DateTime.Today;
    public DateTime? DataRetorno { get; set; }
    
    public int LivroId { get; set; }
    public Livro Livro { get; set; }
    
    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    
}
