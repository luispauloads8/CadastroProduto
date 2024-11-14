using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface IClienteRepository
{
    Task<IEnumerable<Cliente>> GetClienteAsync();
    Task<Cliente> GetByIdAsync(int? id);
    Task<Cliente> CreateAsync(Cliente cliente);
    Task<Cliente> UpdateAsync(Cliente cliente);
    Task<Cliente> DeleteAsync(Cliente cliente);
}
