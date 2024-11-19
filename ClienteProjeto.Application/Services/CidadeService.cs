using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
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

    public Task Add(CidadeDTO cidadeDTO)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<CidadeDTO> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CidadeDTO>> GetCidades()
    {
        throw new NotImplementedException();
    }

    public Task Update(CidadeDTO cidadeDTO)
    {
        throw new NotImplementedException();
    }
}
