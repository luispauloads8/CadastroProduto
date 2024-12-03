using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class EmpresaService : IEmpresaService
{
    private IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public EmpresaService(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public Task Add(EmpresaDTO empresaDTO)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<EmpresaDTO> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<EmpresaDTO>> GetEmpresas()
    {
        throw new NotImplementedException();
    }

    public Task Update(EmpresaDTO empresaDTO)
    {
        throw new NotImplementedException();
    }

    public Task EnsureConnectionOpenAsync()
    {
        throw new NotImplementedException();
    }
}
