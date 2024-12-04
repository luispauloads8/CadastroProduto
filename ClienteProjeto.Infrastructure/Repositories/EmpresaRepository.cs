using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data;

namespace ClienteProjeto.Infrastructure.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _empresaContext;

    public EmpresaRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext empresaContext)
    {
        _contextFactory = contextFactory;
        _empresaContext = empresaContext;
    }

    public async Task<Empresa> CreateAsync(Empresa empresa)
    {
        _empresaContext.Add(empresa);
        await _empresaContext.SaveChangesAsync();
        return empresa;
    }

    public async Task<Empresa> DeleteAsync(Empresa empresa)
    {
        _empresaContext.Remove(empresa);
        await _empresaContext.SaveChangesAsync();
        return empresa;
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

    public async Task<Empresa> GetByIdAsync(int? id)
    {
        return await _empresaContext.Empresas.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Empresa>> GetEmpresaAsync()
    {
        return await _empresaContext.Empresas.AsNoTracking().ToListAsync();
    }

    public async Task<Empresa> UpdateAsync(Empresa empresa)
    {
        var local = _empresaContext.Set<Empresa>().Local
                .FirstOrDefault(entry => entry.Id == empresa.Id);

        if (local != null)
        {
            _empresaContext.Entry(local).State = EntityState.Detached;
        }
        _empresaContext.Entry(empresa).State = EntityState.Modified;

        _empresaContext.Update(empresa);
        await _empresaContext.SaveChangesAsync();
        return empresa;
    }
}
