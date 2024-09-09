using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.DataBase.Services;

public interface IBookService
{
    Task<List<Livro>> GetBooks(string? searchTerm = null);
    Task<bool> AddBook(Livro livro, Autor autor, Categoria categoria);
    Task UpdateBook(Livro livro);
    Task DeleteBook(int id);
}

public class BookService : IBookService
{
    private readonly BibliotecaDbContext _context;

    public BookService(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Livro>> GetBooks(string? searchTerm = null)
    {
        var query = _context.Livros.Include(l => l.Autor).Include(l => l.Categoria).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm)) query = query.Where(l => l.Titulo.Contains(searchTerm));

        return await query.ToListAsync();
    }

    // Create
    public async Task<bool> AddBook(Livro livro, Autor autor, Categoria categoria)
    {
        if (livro == null) return false;

        // Verifica se o livro já existe
        var existingBook = await _context.Livros
            .Where(u => u.Titulo == livro.Titulo)
            .FirstOrDefaultAsync();

        if (existingBook != null) return false;
        // Adiciona e salva autor e categoria
        if (autor != null && categoria != null)
        {
            _context.Autores.Add(autor);
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();


            livro.AutorId = autor.Id;
            livro.CategoryId = categoria.Id;
        }

        // Adiciona o livro
        _context.Livros.Add(livro);
        await _context.SaveChangesAsync();

        return true;
    }


    // Update
    public async Task UpdateBook(Livro livro)
    {
        var existingBook = await _context.Livros.FindAsync(livro.Id);

        if (existingBook != null)
        {
            existingBook.Titulo = livro.Titulo;
            existingBook.PublishedDate = livro.PublishedDate;
            existingBook.AutorId = livro.AutorId;
            existingBook.CategoryId = livro.CategoryId;

            _context.Livros.Update(existingBook);
            await _context.SaveChangesAsync();
        }
    }

    // Delete
    public async Task DeleteBook(int id)
    {
        var book = await _context.Livros.FindAsync(id);

        if (book != null)
        {
            _context.Livros.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}