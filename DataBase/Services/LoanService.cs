using Biblioteca.DataBase;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

public interface ILoanService
{
    Task<List<Emprestimo>> GetLoans(string? searchTerm = null);
    Task<bool> LendBook(Emprestimo emprestimo);
    Task<bool> UpdateLoan(Emprestimo emprestimo, Guid id);
    Task<bool> DeleteLoan(Guid id);
}

public class LoanService : ILoanService
{

    private readonly BibliotecaDbContext _context;

    public LoanService(BibliotecaDbContext context)
    {
        _context = context;
    }
    public async Task<List<Emprestimo>> GetLoans(string? searchTerm = null)
    {

        var query = _context.Emprestimos
                            .Include(e => e.Cliente)
                            .Include(e => e.Livro)
                            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(e => e.Cliente.Nome.Contains(searchTerm)); // Busca pelo nome do cliente
        }

        return await query.ToListAsync();
    }

    public async Task<bool> LendBook(Emprestimo emprestimo)
    {
        if (emprestimo == null || emprestimo.Livro == null || string.IsNullOrEmpty(emprestimo.Cliente?.Nome))
        {
            return false;
        }

        // Verifica se o livro já está emprestado
        var existingLoan = await _context.Emprestimos
            .FirstOrDefaultAsync(e => e.Livro.Id == emprestimo.Livro.Id && e.DataRetorno == null);

        if (existingLoan != null)
        {
            // Livro já está emprestado e não foi devolvido
            return false;
        }

        // Carrega o livro do banco de dados
        var livro = await _context.Livros.FindAsync(emprestimo.Livro.Id);
        if (livro == null)
        {
            return false;
        }

        // Carrega o cliente do banco de dados usando o nome
        var cliente = await _context.Clientes
            .FirstOrDefaultAsync(c => c.Nome == emprestimo.Cliente.Nome);

        if (cliente == null)
        {
            return false;
        }

        // Define o livro e cliente do empréstimo
        emprestimo.Livro = livro;
        emprestimo.Cliente = cliente;
        emprestimo.DataEmprestimo = DateTime.Now;

        // Adiciona o novo empréstimo
        _context.Emprestimos.Add(emprestimo);

        // Salva as mudanças no banco de dados
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }



    public async Task<bool> UpdateLoan(Emprestimo emprestimo, Guid id)
    {
        if (emprestimo == null || emprestimo.Livro == null || string.IsNullOrEmpty(emprestimo.Cliente?.Nome))
        {
            return false;
        }

        // Verifica se o empréstimo existe
        var existingLoan = await _context.Emprestimos
            .Include(e => e.Livro)
            .Include(e => e.Cliente)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existingLoan == null)
        {
            return false;
        }

        // Atualiza os detalhes do empréstimo
        existingLoan.Cliente.Nome = emprestimo.Cliente.Nome;
        existingLoan.Livro.Titulo = emprestimo.Livro.Titulo;
        existingLoan.DataRetorno = emprestimo.DataRetorno;

        // Não precisa alterar o livro e cliente se são os mesmos objetos
        _context.Emprestimos.Update(existingLoan);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteLoan(Guid id)
    {
        var emprestimo = await _context.Emprestimos.FindAsync(id);

        if (emprestimo == null)
        {
            return false;
        }

        _context.Emprestimos.Remove(emprestimo);

        var result = await _context.SaveChangesAsync();

        return result > 0;
    }


}