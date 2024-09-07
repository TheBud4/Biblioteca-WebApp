namespace Biblioteca.Models;

public class Endereco {
    
    public int Id { get; set; }
    
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Bairro { get; set; }
    public string CEP { get; set; }
    public string Complemento { get; set; }
    
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
}