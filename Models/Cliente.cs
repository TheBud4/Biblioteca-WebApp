namespace Biblioteca.Models;

public class Cliente : Usuario {
    public List<Emprestimo> Emprestimos { get; set; }
}