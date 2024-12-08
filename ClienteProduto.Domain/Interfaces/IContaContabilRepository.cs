using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface IContaContabilRepository
{
    Task<IEnumerable<ContaContabil>> GetClienteAsync();
    Task<ContaContabil> GetByIdAsync(int? id);
    Task<ContaContabil> CreateAsync(ContaContabil contaContabil);
    Task<ContaContabil> UpdateAsync(ContaContabil contaContabil);
    Task<ContaContabil> DeleteAsync(ContaContabil contaContabil);
    Task EnsureConnectionOpenAsync();
}
