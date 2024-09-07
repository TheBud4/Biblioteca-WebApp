namespace Biblioteca.Models;

public class Funcionario: Usuario {
    public float Salario { get; set; }
    
    public ICollection<Emprestimo> Emprestimos { get; set; }
}