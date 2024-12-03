using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class ProdutoServicoService : IProdutoServicoService
{
    private IProdutoServicoRepository _produtoServicoRepository;
    private readonly IMapper _mapper;

    public ProdutoServicoService(IProdutoServicoRepository produtoServicoRepository, IMapper mapper)
    {
        _produtoServicoRepository = produtoServicoRepository ??
            throw new ArgumentNullException(nameof(produtoServicoRepository));
        _mapper = mapper;
    }

    public async Task Add(ProdutoServicoDTO produtoServicoDTO)
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();
        var produtoEntiy =  _mapper.Map<ProdutoServico>(produtoServicoDTO);
        await _produtoServicoRepository.CreateAsync(produtoEntiy);
    }

    public async Task Delete(int? id)
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();
        var produto = _produtoServicoRepository.GetByIdAsync(id).Result;
        await _produtoServicoRepository.DeleteAsync(produto);
    }

    public async Task<ProdutoServicoDTO> GetById(int? id)
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();
        var produto = await _produtoServicoRepository.GetByIdAsync(id);
        return _mapper.Map<ProdutoServicoDTO>(produto);
    }

    public async Task<IEnumerable<ProdutoServicoDTO>> GetProdutosServicos()
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();
        var produtoEntity = await _produtoServicoRepository.GetProdutoServicoAsync();
        return _mapper.Map<IEnumerable<ProdutoServicoDTO>>(produtoEntity);
    }

    public async Task Update(ProdutoServicoDTO produtoDTO)
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();
        var produtoEntity = _mapper.Map<ProdutoServico>(produtoDTO);
        await _produtoServicoRepository.UpdateAsync(produtoEntity);
    }
}
