using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class CidadeService : ICidadeService
{
    private ICidadeRepository _cidadeRepository;
    private readonly IMapper _mapper;

    public CidadeService(ICidadeRepository cidadeRepository, IMapper mapper)
    {
        _cidadeRepository = cidadeRepository;
        _mapper = mapper;
    }

    public async Task Add(CidadeDTO cidadeDTO)
    {
        await _cidadeRepository.EnsureConnectionOpenAsync();
        var cidadeEntity = _mapper.Map<Cidade>(cidadeDTO);
        await _cidadeRepository.CreateAsync(cidadeEntity);
    }

    public async Task Delete(int? id)
    {
        await _cidadeRepository.EnsureConnectionOpenAsync();
        var cidadeEntity = await _cidadeRepository.GetByIdAsync(id);
        await _cidadeRepository.DeleteAsync(cidadeEntity);
    }

    public async Task<CidadeDTO> GetById(int? id)
    {
        await _cidadeRepository.EnsureConnectionOpenAsync();
        var cidadeEntity = await _cidadeRepository.GetByIdAsync(id);
        return _mapper.Map<CidadeDTO>(cidadeEntity);
    }

    public async Task<IEnumerable<CidadeDTO>> GetCidades()
    {
        await _cidadeRepository.EnsureConnectionOpenAsync();
        var cidadesEntity = await _cidadeRepository.GetCidadeAsync();
        return _mapper.Map<IEnumerable<CidadeDTO>>(cidadesEntity);
    }

    public async Task Update(CidadeDTO cidadeDTO)
    {
        await _cidadeRepository.EnsureConnectionOpenAsync();
        var cidadeEntity = _mapper.Map<Cidade>(cidadeDTO);
        await _cidadeRepository.UpdateAsync(cidadeEntity);
    }
}
