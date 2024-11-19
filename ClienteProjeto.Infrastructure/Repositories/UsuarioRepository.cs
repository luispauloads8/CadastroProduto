using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    public Task<Usuario> CreateAsync(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> DeleteAsync(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Usuario>> GetUsuarioAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Usuario> UpdateAsync(Usuario usuario)
    {
        throw new NotImplementedException();
    }
}
