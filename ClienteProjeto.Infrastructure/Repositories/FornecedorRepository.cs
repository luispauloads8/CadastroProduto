using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClienteProjeto.Infrastructure.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _fornecedorContext;

    public FornecedorRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext fornecedorContext)
    {
        _contextFactory = contextFactory;
        _fornecedorContext = fornecedorContext;
    }

    public async Task<Fornecedor> CreateAsync(Fornecedor fornecedor)
    {
        _fornecedorContext.Add(fornecedor);
        await _fornecedorContext.SaveChangesAsync();
        return fornecedor;
    }

    public async Task<Fornecedor> DeleteAsync(Fornecedor fornecedor)
    {
        _fornecedorContext.Remove(fornecedor);
        await _fornecedorContext.SaveChangesAsync() ;
        return fornecedor;
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

    public async Task<Fornecedor> GetByIdAsync(int? id)
    {
        return await _fornecedorContext.Fornecedores.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<IEnumerable<Fornecedor>> GetFornecedorAsync()
    {
        return await _fornecedorContext.Fornecedores.AsNoTracking().ToListAsync();
    }

    public async Task<Fornecedor> UpdateAsync(Fornecedor fornecedor)
    {
        var local = _fornecedorContext.Set<Fornecedor>().Local
                .FirstOrDefault(entry => entry.Id == fornecedor.Id);

        if (local != null)
        {
            _fornecedorContext.Entry(local).State = EntityState.Detached;
        }
        _fornecedorContext.Entry(fornecedor).State = EntityState.Modified;

        _fornecedorContext.Update(fornecedor);
        await _fornecedorContext.SaveChangesAsync();
        return fornecedor;

    }
}
