using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class FornecedorService : IFornecedorService
{
    private IFornecedorRepository _fornecedorRepository;
    private readonly IMapper _mapper;

    public FornecedorService(IFornecedorRepository fornecedorRepository, IMapper mapper)
    {
        _fornecedorRepository = fornecedorRepository;
        _mapper = mapper;
    }

    public Task Add(FornecedorDTO fornecedorDTO)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<FornecedorDTO> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FornecedorDTO>> GetFornecedores()
    {
        throw new NotImplementedException();
    }

    public Task Update(FornecedorDTO fornecedorDTO)
    {
        throw new NotImplementedException();
    }
}
