using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class UsuarioService : IUsuarioService
{
    private IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public Task Add(UsuarioDTO usuarioDTO)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<UsuarioDTO> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UsuarioDTO>> GetUsuarios()
    {
        throw new NotImplementedException();
    }

    public Task Update(UsuarioDTO usuarioDTO)
    {
        throw new NotImplementedException();
    }

}
