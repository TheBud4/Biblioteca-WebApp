using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.DataBase.Services;

public enum TipoUsuario
{
    Todos,
    Funcionario,
    Cliente
}

public interface IUserService
{
    Task<bool> Login(string username, string password);
    Task<bool> RegisterCliente(Cliente cliente, Endereco endereco);
    Task<bool> RegisterFuncionario(Funcionario funcionario, Endereco endereco);
    Task<List<Usuario>> GetUsers(string? searchTerm = null);
    Task<List<Funcionario>> GetFuncionarios(string? searchTerm = null);
    Task<List<Cliente>> GetClientes(string? searchTerm = null);
}


public class UserService : IUserService
{
    private readonly BibliotecaDbContext _context;

    public UserService(BibliotecaDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Login(string username, string password)
    {
        var user = await _context.Usuarios
            .Where(u => u.Nome == username && u.Senha == password)
            .FirstOrDefaultAsync();

        return user != null;
    }

    public async Task<bool> RegisterCliente(Cliente cliente, Endereco endereco)
    {
        cliente.TipoUsuario = TipoUsuario.Cliente.ToString();

        // Verifica se o funcionário já existe
        var existingUser = await _context.Clientes
            .Where(u => u.Nome == cliente.Nome && u.TipoUsuario == cliente.TipoUsuario)
            .FirstOrDefaultAsync();

        if (existingUser != null) return false;

        // Adiciona o endereço e associa ao funcionário
        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync(); // Salva o endereço para gerar o ID

        cliente.EnderecoId = endereco.Id;
        _context.Clientes.Add(cliente);
        await _context.SaveChangesAsync(); // Salva o cliente

        return true;
    }

    public async Task<bool> RegisterFuncionario(Funcionario funcionario, Endereco endereco)
    {
        funcionario.TipoUsuario = TipoUsuario.Funcionario.ToString();

        // Verifica se o funcionário já existe
        var existingUser = await _context.Funcionarios
            .Where(u => u.Nome == funcionario.Nome && u.TipoUsuario == funcionario.TipoUsuario)
            .FirstOrDefaultAsync();

        if (existingUser != null) return false;

        // Adiciona o endereço e associa ao funcionário
        _context.Enderecos.Add(endereco);
        await _context.SaveChangesAsync(); // Salva o endereço para gerar o ID

        funcionario.EnderecoId = endereco.Id;
        _context.Funcionarios.Add(funcionario);
        await _context.SaveChangesAsync(); // Salva o funcionário

        return true;
    }

    public async Task<List<Usuario>> GetUsers(string? searchTerm = null)
    {
        var query = _context.Usuarios.Include(u => u.Endereco).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.Nome.Contains(searchTerm) || u.Email.Contains(searchTerm));
        }

        return await query.ToListAsync();
    }

    public async Task<List<Funcionario>> GetFuncionarios(string? searchTerm = null)
    {
        var query = _context.Funcionarios.Include(u => u.Endereco).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.Nome.Contains(searchTerm) || u.Email.Contains(searchTerm));
        }

        return await query.ToListAsync();
    }

    public async Task<List<Cliente>> GetClientes(string? searchTerm = null)
    {
        var query = _context.Clientes.Include(u => u.Endereco).AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(u => u.Nome.Contains(searchTerm) || u.Email.Contains(searchTerm));
        }

        return await query.ToListAsync();
    }
}
