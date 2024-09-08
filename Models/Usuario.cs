using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Usuario {
  public Guid Id { get; set; } = Guid.NewGuid();

  [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
  [StringLength(96)]

  public string Nome { get; set; }

  [EmailAddress]
  public string Email { get; set; }

  [Required(ErrorMessage = "A senha é obrigatória.")]
  public string Senha { get; set; }

  public DateTime DataEntrada { get; set; } = DateTime.Now;

  public int? EnderecoId { get; set; }
  public Endereco Endereco { get; set; }

  public string TipoUsuario { get; set; }
}