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

    public async Task<ProdutoServico> CreateAsync(ProdutoServico produtoServico)
    {
         _produtoServicoContext.Add(produtoServico);
        await _produtoServicoContext.SaveChangesAsync();
        return produtoServico;
    }

    public async Task<ProdutoServico> GetByIdAsync(int? id)
    {
        return await _produtoServicoContext.ProdutoServicos.Include(c => c.Categoria)
                .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<ProdutoServico>> GetProdutoServicoAsync()
    {
        return await _produtoServicoContext.ProdutoServicos.AsNoTracking().ToListAsync();
    }

    public async Task<ProdutoServico> DeleteAsync(ProdutoServico produtoServico)
    {
        _produtoServicoContext.Remove(produtoServico);
        await _produtoServicoContext.SaveChangesAsync();
        return produtoServico;
    }

    public async Task<ProdutoServico> UpdateAsync(ProdutoServico produtoServico)
    {
        var local = _produtoServicoContext.Set<ProdutoServico>().Local
                        .FirstOrDefault(entry => entry.Id == produtoServico.Id); 

        if (local != null)
        {
            _produtoServicoContext.Entry(local).State = EntityState.Detached;
        }
        _produtoServicoContext.Entry(produtoServico).State = EntityState.Modified;

        _produtoServicoContext.Update(produtoServico);
        await _produtoServicoContext.SaveChangesAsync();
        return produtoServico;
    }
}
