using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Infrastructure.Repositories;

public class ContaContabilRepository : IContaContabilRepository
{
    public Task<Cliente> CreateAsync(Cliente cliente)
    {
        throw new NotImplementedException();
    }

    public Task<Cliente> DeleteAsync(Cliente cliente)
    {
        throw new NotImplementedException();
    }

    public Task EnsureConnectionOpenAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Cliente> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Cliente>> GetClienteAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Cliente> UpdateAsync(Cliente cliente)
    {
        throw new NotImplementedException();
    }
}
