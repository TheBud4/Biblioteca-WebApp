using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models;

public class Livro {
    public int Id { get; set; }

    [Required(ErrorMessage = "O título do livro é obrigatório.")]
    public string Titulo { get; set; }

    public DateTime PublishedDate { get; set; } = DateTime.Now;

    public int? AutorId { get; set; }
    public Autor Autor { get; set; }

    public int? CategoryId { get; set; }
    public Categoria Categoria { get; set; }
}