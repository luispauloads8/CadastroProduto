using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetUsuarioAsync();
    Task<Usuario> GetByIdAsync(int? id);
    Task<Usuario> CreateAsync(Usuario usuario);
    Task<Usuario> UpdateAsync(Usuario usuario);
    Task<Usuario> DeleteAsync(Usuario usuario);
}
