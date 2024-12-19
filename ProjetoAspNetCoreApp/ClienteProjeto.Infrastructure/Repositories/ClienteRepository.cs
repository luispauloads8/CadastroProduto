using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClienteProjeto.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _clienteContext;

    public ClienteRepository(IDbContextFactory<ApplicationDbContext> contextFactory, ApplicationDbContext clienteContext)
    {
        _contextFactory = contextFactory;
        _clienteContext = clienteContext;
    }

    public async Task<Cliente> CreateAsync(Cliente cliente)
    {
        _clienteContext.Add(cliente);
        await _clienteContext.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> DeleteAsync(Cliente cliente)
    {
        _clienteContext.Remove(cliente);
        await _clienteContext.SaveChangesAsync();
        return cliente;
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

    public async Task<Cliente> GetByIdAsync(int? id)
    {
        return await _clienteContext.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Cliente>> GetClienteAsync()
    {
        return await _clienteContext.Clientes.AsNoTracking().ToListAsync();
    }

    public async Task<Cliente> UpdateAsync(Cliente cliente)
    {
        var local = _clienteContext.Set<Cliente>().Local
                  .FirstOrDefault(entry => entry.Id == cliente.Id);

        if (local != null)
        {
            _clienteContext.Entry(local).State = EntityState.Detached;
        }
        _clienteContext.Entry(cliente).State = EntityState.Modified;

        _clienteContext.Update(cliente);
        await _clienteContext.SaveChangesAsync();
        return cliente;
    }
}
