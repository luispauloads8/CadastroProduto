using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class LancamentoService : ILancamentoService
{
    private ILancamentoRepository _lancamentoRepository;
    private readonly IMapper _mapper;

    public LancamentoService(ILancamentoRepository lancamentoRepository, IMapper mapper)
    {
        _lancamentoRepository = lancamentoRepository;
        _mapper = mapper;
    }

    public  async Task Add(LancamentoDTO lancamentoDTO)
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamentoEntity = _mapper.Map<Lancamento>(lancamentoDTO);
        await _lancamentoRepository.CreateAsync(lancamentoEntity);
    }

    public async Task Delete(int? id)
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamentoEntity = await _lancamentoRepository.GetByIdAsync(id);
        await _lancamentoRepository.DeleteAsync(lancamentoEntity);
    }

    public async Task<LancamentoDTO> GetById(int? id)
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamentoEntity = await _lancamentoRepository.GetByIdAsync(id);
        return _mapper.Map<LancamentoDTO>(lancamentoEntity);
    }

    public async Task<IEnumerable<LancamentoDTO>> GetLancamentos()
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamentosEntity = _lancamentoRepository.GetLancamentoAsync().Result;
        return _mapper.Map<IEnumerable<LancamentoDTO>>(lancamentosEntity);
    }

    public async Task Update(LancamentoDTO lancamentoDTO)
    {
        await _lancamentoRepository.EnsureConnectionOpenAsync();
        var lancamento = _mapper.Map<Lancamento>(lancamentoDTO);
        await _lancamentoRepository.UpdateAsync(lancamento);
    }

}
