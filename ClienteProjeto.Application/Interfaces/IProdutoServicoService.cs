using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Infrastructure.Context;

namespace ClienteProjeto.Application.Interfaces;

public interface IProdutoServicoService
{
    Task<IEnumerable<ProdutoServicoDTO>> GetProdutosServicos(); 
    Task<ProdutoServicoDTO> GetById(int? id);
    Task Add(ProdutoServicoDTO produtoServicoDTO);
    Task Update(ProdutoServicoDTO produtoDTO);
    Task Delete(int? id);
    Task EnsureConnectionOpenAsync();
}
