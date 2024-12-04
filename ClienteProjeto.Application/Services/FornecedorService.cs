using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
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

    public async Task Add(FornecedorDTO fornecedorDTO)
    {
        await _fornecedorRepository.EnsureConnectionOpenAsync();
        var fornecedor = _mapper.Map<Fornecedor>(fornecedorDTO);
        await _fornecedorRepository.CreateAsync(fornecedor);
    }

    public async Task Delete(int? id)
    {
        await _fornecedorRepository.EnsureConnectionOpenAsync();
        var fornecedor = _fornecedorRepository.GetByIdAsync(id).Result;
        await _fornecedorRepository.DeleteAsync(fornecedor);
    }

    public async Task<FornecedorDTO> GetById(int? id)
    {
        await _fornecedorRepository.EnsureConnectionOpenAsync();
        var fornecedorEntity = await _fornecedorRepository.GetByIdAsync(id);
        return _mapper.Map<FornecedorDTO>(fornecedorEntity);
    }

    public async Task<IEnumerable<FornecedorDTO>> GetFornecedores()
    {
        await _fornecedorRepository.EnsureConnectionOpenAsync();
        var fornecedoresEntity = await _fornecedorRepository.GetFornecedorAsync();
        return _mapper.Map<IEnumerable<FornecedorDTO>>(fornecedoresEntity);
    }

    public async Task Update(FornecedorDTO fornecedorDTO)
    {
        await _fornecedorRepository.EnsureConnectionOpenAsync();
        var fornecedorEntity = _mapper.Map<Fornecedor>(fornecedorDTO);
        await _fornecedorRepository.UpdateAsync(fornecedorEntity);
    }
}
