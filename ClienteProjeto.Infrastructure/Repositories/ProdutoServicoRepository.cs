using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Infrastructure.Repositories;

public class ProdutoServicoRepository : IProdutoServicoRepository
{
    public Task<ProdutoServico> CreateAsync(ProdutoServico produtoServico)
    {
        throw new NotImplementedException();
    }

    public Task<ProdutoServico> DeleteAsync(ProdutoServico produtoServico)
    {
        throw new NotImplementedException();
    }

    public Task<ProdutoServico> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProdutoServico>> GetProdutoServicoAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProdutoServico> UpdateAsync(ProdutoServico produtoServico)
    {
        throw new NotImplementedException();
    }
}
