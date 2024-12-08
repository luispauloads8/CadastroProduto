using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
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

    public async Task Add(GrupoContaDTO grupoContaDTO)
    {
        await _grupoContaRepository.EnsureConnectionOpenAsync();
        var grupoContaEntity = _mapper.Map<GrupoConta>(grupoContaDTO);
        await _grupoContaRepository.CreateAsync(grupoContaEntity);

    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public async Task<GrupoContaDTO> GetByID(int? id)
    {
        await _grupoContaRepository.EnsureConnectionOpenAsync();
        var grupoContaEntity = _grupoContaRepository.GetByIdAsync(id).Result;
        return _mapper.Map<GrupoContaDTO>(grupoContaEntity);
    }

    public async Task<IEnumerable<GrupoContaDTO>> GetGrupoContas()
    {
        await _grupoContaRepository.EnsureConnectionOpenAsync();
        var grupoContas = await _grupoContaRepository.GetGrupoContaAsync();
        return _mapper.Map<IEnumerable<GrupoContaDTO>>(grupoContas);

    }

    public Task Update(GrupoContaDTO dto)
    {
        throw new NotImplementedException();
    }

}
