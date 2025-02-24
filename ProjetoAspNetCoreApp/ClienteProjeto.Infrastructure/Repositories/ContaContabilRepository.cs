using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClienteProjeto.Infrastructure.Repositories;

public class ContaContabilRepository : IContaContabilRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _contaConbailContext;

    public ContaContabilRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext contaConbailContext)
    {
        _contextFactory = contextFactory;
        _contaConbailContext = contaConbailContext;
    }

    public async Task<ContaContabil> CreateAsync(ContaContabil contaContabil)
    {
        _contaConbailContext.Add(contaContabil);
        await _contaConbailContext.SaveChangesAsync();
        return contaContabil;
    }

    public async Task<ContaContabil> DeleteAsync(ContaContabil contaContabil)
    {
        _contaConbailContext.Remove(contaContabil);
        await _contaConbailContext.SaveChangesAsync();
        return contaContabil;
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

    public async Task<ContaContabil> GetByIdAsync(int? id)
    {
       return await _contaConbailContext.ContaContabeis.FindAsync(id);
    }

    public async Task<IEnumerable<ContaContabil>> GetClienteAsync()
    {
        return await _contaConbailContext.ContaContabeis.AsTracking().ToListAsync();
    }

    public async Task<ContaContabil> UpdateAsync(ContaContabil contaContabil)
    {
        var local = _contaConbailContext.Set<ContaContabil>().Local
          .FirstOrDefault(entry => entry.Id == contaContabil.Id);

        if (local != null)
        {
            _contaConbailContext.Entry(local).State = EntityState.Detached;
        }
        _contaConbailContext.Entry(contaContabil).State = EntityState.Modified;

        _contaConbailContext.Update(contaContabil);
        await _contaConbailContext.SaveChangesAsync();
        return contaContabil;
    }

    public Task<List<ContaContabil>> GetContaContabilTermoAsync(string search)
    {
        return _contaConbailContext.ContaContabeis
            .Where(c => c.Descricao.Contains(search))
            .ToListAsync();
    }
}
