using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Application.Interfaces;

public interface IProdutoServicoService
{
    Task<IEnumerable<ProdutoServicoDTO>> GetProdutosServicos(); 
    Task<ProdutoServicoDTO> GetById(int? id);
    Task<List<ProdutoServicoDTO>> GetProdutoServicoTermo(string search);
    Task Add(ProdutoServicoDTO produtoServicoDTO);
    Task Update(ProdutoServicoDTO produtoDTO);
    Task Delete(int? id);
    Task<PagedResult<ProdutoServicoDTO>> ObterPaginadoAsync(int pageNumber, int pageSize);
}
