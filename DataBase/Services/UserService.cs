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
    Task<bool> ExcluirUsuario(Guid id);
    Task<bool> EditCliente(Guid id, Cliente cliente);
    Task<bool> EditFuncionario(Guid id, Funcionario funcionario);
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

        // Verifica se o cliente já existe
        var existingUser = await _context.Clientes
            .Where(u => u.Nome == cliente.Nome && u.TipoUsuario == cliente.TipoUsuario)
            .FirstOrDefaultAsync();

        if (existingUser != null) return false;

        // Adiciona o endereço e associa ao cliente
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

    public async Task<bool> ExcluirUsuario(Guid id)
    {
        var user = await _context.Usuarios
            .Include(u => u.Endereco)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return false;

        if (user.Endereco != null)  // Exclui o endereço se existir
        {
            _context.Enderecos.Remove(user.Endereco);
        }

        _context.Usuarios.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> EditCliente(Guid id, Cliente clienteAtualizado)
    {
        // Buscar o cliente no banco de dados
        var cliente = await _context.Usuarios.Include(u => u.Endereco)
                                             .FirstOrDefaultAsync(u => u.Id == id);

        if (cliente == null) return false;

        // Atualizar as propriedades do cliente
        cliente.Nome = clienteAtualizado.Nome;
        cliente.Email = clienteAtualizado.Email;
        // Atualizar outras propriedades do cliente, se necessário...

        // Atualizar o endereço do cliente
        if (cliente.Endereco != null)
        {
            cliente.Endereco.Rua = clienteAtualizado.Endereco.Rua;
            cliente.Endereco.Bairro = clienteAtualizado.Endereco.Bairro;
            cliente.Endereco.CEP = clienteAtualizado.Endereco.CEP;
            cliente.Endereco.Complemento = clienteAtualizado.Endereco.Complemento;
            cliente.Endereco.Numero = clienteAtualizado.Endereco.Numero;
        }
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> EditFuncionario(Guid id, Funcionario funcionarioAtualizado)
    {
        // Buscar o cliente no banco de dados
        var cliente = await _context.Usuarios.Include(u => u.Endereco)
                                             .FirstOrDefaultAsync(u => u.Id == id);

        if (cliente == null) return false;

        // Atualizar as propriedades do cliente
        cliente.Nome = funcionarioAtualizado.Nome;
        cliente.Email = funcionarioAtualizado.Email;
        // Atualizar outras propriedades do cliente, se necessário...

        // Atualizar o endereço do cliente
        if (cliente.Endereco != null)
        {
            cliente.Endereco.Rua = funcionarioAtualizado.Endereco.Rua;
            cliente.Endereco.Bairro = funcionarioAtualizado.Endereco.Bairro;
            cliente.Endereco.CEP = funcionarioAtualizado.Endereco.CEP;
            cliente.Endereco.Complemento = funcionarioAtualizado.Endereco.Complemento;
            cliente.Endereco.Numero = funcionarioAtualizado.Endereco.Numero;
        }
        await _context.SaveChangesAsync();

        return true;
    }

}
