using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
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

    public Task Add(LancamentoDTO lancamentoDTO)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<LancamentoDTO> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LancamentoDTO>> GetLancamentos()
    {
        throw new NotImplementedException();
    }

    public Task Update(LancamentoDTO lancamentoDTO)
    {
        throw new NotImplementedException();
    }

    public Task EnsureConnectionOpenAsync()
    {
        throw new NotImplementedException();
    }
}
