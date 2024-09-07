using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.DataBase.Services;

public enum TipoUsuario {
    Todos,
    Funcionario,
    Cliente
}

public interface IUserService {
    Task<bool> Login(string username, string password);
    Task<bool> Register(Usuario usuario);
    Task<List<Usuario>> GetUsers(TipoUsuario tipoUsuario);
}

public class UserService : IUserService {
    private readonly BibliotecaDbContext _context;

    public UserService(BibliotecaDbContext context) {
        _context = context;
    }

    public async Task<bool> Login(string username, string password) {
        var user = await _context.Usuarios
            .Where(u => u.Nome == username && u.Senha == password)
            .FirstOrDefaultAsync();

        return user != null;
    }

    public async Task<bool> Register(Usuario usuario) {
        var existingUser = await _context.Usuarios
            .Where(u => u.Nome == usuario.Nome)
            .FirstOrDefaultAsync();

        if (existingUser != null) return false;

        _context.Usuarios.Add(usuario);

        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Usuario>> GetUsers(TipoUsuario tipoUsuario) {
        
        if (tipoUsuario == TipoUsuario.Todos) {
            return await _context.Usuarios.ToListAsync();
        }
        return await _context.Usuarios
            .Where(u => u.TipoUsuario == tipoUsuario.ToString())
            .ToListAsync();
    }

}