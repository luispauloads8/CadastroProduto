using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface IUsuarioService
{
    Task<IEnumerable<UsuarioDTO>> GetUsuarios();
    Task<UsuarioDTO> GetById(int? id);
    Task Add(UsuarioDTO usuarioDTO);
    Task Update(UsuarioDTO usuarioDTO);
    Task Delete(int? id);
    Task EnsureConnectionOpenAsync();
}
