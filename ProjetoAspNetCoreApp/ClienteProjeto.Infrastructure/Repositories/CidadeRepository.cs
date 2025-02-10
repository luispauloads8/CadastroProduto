using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClienteProjeto.Infrastructure.Repositories;

public class CidadeRepository : ICidadeRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _cidadeContext;

    public CidadeRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext produtoServicoContext)
    {
        _contextFactory = contextFactory;
        _cidadeContext = produtoServicoContext;
    }

    public async Task<Cidade> CreateAsync(Cidade cidade)
    {
        _cidadeContext.Add(cidade);
        await _cidadeContext.SaveChangesAsync();
        return cidade;
    }

    public async Task<Cidade> DeleteAsync(Cidade cidade)
    {
         _cidadeContext.Cidades.Remove(cidade);
        await _cidadeContext.SaveChangesAsync();
        return cidade;
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

    public async Task<Cidade> GetByIdAsync(int? id)
    {
        return  await _cidadeContext.Cidades.FindAsync(id);
    }

    public async Task<IEnumerable<Cidade>> GetCidadeAsync()
    {
        return await _cidadeContext.Cidades.AsNoTracking().ToListAsync();
    }

    public async Task<Cidade> UpdateAsync(Cidade cidade)
    {
        var local = _cidadeContext.Set<Cidade>().Local
                .FirstOrDefault(entry => entry.Id == cidade.Id);

        if (local != null)
        {
            _cidadeContext.Entry(local).State = EntityState.Detached;
        }
        _cidadeContext.Entry(cidade).State = EntityState.Modified;

        _cidadeContext.Update(cidade);
        await _cidadeContext.SaveChangesAsync();
        return cidade;
    }

    public async Task<List<Cidade>> GetCidadeTermoAsync(string termo)
    {
        return await _cidadeContext.Cidades
           .Where(p => p.Descricao.Contains(termo))
           .ToListAsync();
    }
}
