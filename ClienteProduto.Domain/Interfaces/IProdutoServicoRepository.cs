using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface IProdutoServicoRepository
{
    Task<IEnumerable<ProdutoServico>> GetProdutoServicoAsync();
    Task<ProdutoServico> GetByIdAsync(int? id);
    Task<ProdutoServico> CreateAsync(ProdutoServico produtoServico);
    Task<ProdutoServico> UpdateAsync(ProdutoServico produtoServico);
    Task<ProdutoServico> DeleteAsync(ProdutoServico produtoServico);
}
