using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClienteProjeto.Infrastructure.Repositories;

public class LancamentoRepository : ILancamentoRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _lancamentosContext;

    public LancamentoRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext lancamentosContext)
    {
        _contextFactory = contextFactory;
        _lancamentosContext = lancamentosContext;
    }

    public async Task<Lancamento> CreateAsync(Lancamento lancamento)
    {
        _lancamentosContext.Add(lancamento);
       await _lancamentosContext.SaveChangesAsync();
       return lancamento;
    }

    public async Task<Lancamento> DeleteAsync(Lancamento lancamento)
    {
        _lancamentosContext.Remove(lancamento);
        await _lancamentosContext.SaveChangesAsync();
        return lancamento;
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

    public async Task<Lancamento> GetByIdAsync(int? id)
    {
        return await _lancamentosContext.Lancamentos.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<IEnumerable<Lancamento>> GetLancamentoAsync()
    {
        return await _lancamentosContext.Lancamentos.AsNoTracking().ToListAsync();
    }

    public async Task<Lancamento> UpdateAsync(Lancamento lancamento)
    {
        var local = _lancamentosContext.Set<Fornecedor>().Local
              .FirstOrDefault(entry => entry.Id == lancamento.Id);

        if (local != null)
        {
            _lancamentosContext.Entry(local).State = EntityState.Detached;
        }
        _lancamentosContext.Entry(lancamento).State = EntityState.Modified;

        _lancamentosContext.Update(lancamento);
        await _lancamentosContext.SaveChangesAsync();
        return lancamento;
    }
}
