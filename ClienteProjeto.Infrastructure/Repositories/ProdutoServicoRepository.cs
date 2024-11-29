using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ClienteProjeto.Infrastructure.Repositories;

public class ProdutoServicoRepository : IProdutoServicoRepository
{
    private ApplicationDbContext _produtoServicoContext;

    public ProdutoServicoRepository(ApplicationDbContext produtoServicoContext)
    {
        _produtoServicoContext = produtoServicoContext;
    }

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

    public async Task<IEnumerable<ProdutoServico>> GetProdutoServicoAsync()
    {
        return await _produtoServicoContext.ProdutoServicos.ToListAsync();
    }

    public Task<ProdutoServico> UpdateAsync(ProdutoServico produtoServico)
    {
        throw new NotImplementedException();
    }
}
