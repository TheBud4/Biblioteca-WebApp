namespace Biblioteca.Models;

public class Cliente: Usuario {
    public ICollection<Emprestimo> Emprestimos { get; set; }
}