using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ClienteProjeto.Infrastructure.Repositories;

public class ProdutoServicoRepository : IProdutoServicoRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _produtoServicoContext;

    public ProdutoServicoRepository(ApplicationDbContext produtoServicoContext, IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _produtoServicoContext = produtoServicoContext;
        _contextFactory = contextFactory;
    }

    public async Task<ProdutoServico> CreateAsync(ProdutoServico produtoServico)
    {
         _produtoServicoContext.Add(produtoServico);
        await _produtoServicoContext.SaveChangesAsync();
        return produtoServico;
    }

    public async Task<ProdutoServico> GetByIdAsync(int? id)
    {
        IQueryable<ProdutoServico> query = _produtoServicoContext.ProdutoServicos.Include(p => p.Categoria);


        query = query.AsNoTracking().OrderBy(e => e.Id).Where(p => p.Id == id);

        return  await query.FirstOrDefaultAsync();
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

    public async Task EnsureConnectionOpenAsync()
    {
        var context = _contextFactory.CreateDbContext();
        var connection = context.Database.GetDbConnection();
        Console.WriteLine("Estado atual da conexão: " + connection.State);
        if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
        {
            Console.WriteLine("Tentando abrir a conexão...");
            await connection.OpenAsync();
            Console.WriteLine("Conexão aberta.");
        }
        else if (connection.State == ConnectionState.Connecting)
        {
            Console.WriteLine("A conexão já está em processo de abertura.");
        }
    }

    public async Task<List<ProdutoServico>> GetProdutoServicoTermoAsync(string search)
    {
        return await _produtoServicoContext.ProdutoServicos
            .Where(p => p.Descricao.Contains(search))
            .ToListAsync();
    }
}
