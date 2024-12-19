using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClienteProjeto.Infrastructure.Repositories;

public class GrupoContaRepository : IGrupoContaRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _grupoContasContext;

    public GrupoContaRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext grupoContasContext)
    {
        _contextFactory = contextFactory;
        _grupoContasContext = grupoContasContext;
    }

    public async Task<GrupoConta> CreateAsync(GrupoConta grupoConta)
    {
        _grupoContasContext.Add(grupoConta);
        await _grupoContasContext.SaveChangesAsync();
        return grupoConta;
    }

    public async Task<GrupoConta> DeleteAsync(GrupoConta grupoConta)
    {
        _grupoContasContext.Remove(grupoConta);
        await _grupoContasContext.SaveChangesAsync();
        return grupoConta;
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

    public async Task<GrupoConta> GetByIdAsync(int? id)
    {
        return await _grupoContasContext.GrupoContas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<GrupoConta>> GetGrupoContaAsync()
    {
        return await _grupoContasContext.GrupoContas.AsNoTracking().ToListAsync();
    }

    public async Task<GrupoConta> UpdateAsync(GrupoConta grupoConta)
    {
        var local = _grupoContasContext.Set<Fornecedor>().Local
               .FirstOrDefault(entry => entry.Id == grupoConta.Id);

        if (local != null)
        {
            _grupoContasContext.Entry(local).State = EntityState.Detached;
        }
        _grupoContasContext.Entry(grupoConta).State = EntityState.Modified;

        _grupoContasContext.Update(grupoConta);
        await _grupoContasContext.SaveChangesAsync();
        return grupoConta;
    }
}
