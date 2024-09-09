namespace Biblioteca.Models;

public class Funcionario : Usuario {
    public float Salario { get; set; }

    public List<Emprestimo> Emprestimos { get; set; }
}