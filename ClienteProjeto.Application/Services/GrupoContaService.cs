using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class GrupoContaService : IGrupoContaService
{
    private IGrupoContaRepository _grupoContaRepository;
    private readonly IMapper _mapper;

    public GrupoContaService(IGrupoContaRepository grupoContaRepository, IMapper mapper)
    {
        _grupoContaRepository = grupoContaRepository;
        _mapper = mapper;
    }

    public Task Add(GrupoContaDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<GrupoContaDTO> GetByID(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GrupoContaDTO>> GetGrupoContas()
    {
        throw new NotImplementedException();
    }

    public Task Update(GrupoContaDTO dto)
    {
        throw new NotImplementedException();
    }

}
