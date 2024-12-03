using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface IFornecedorRepository
{
    Task<IEnumerable<Fornecedor>> GetFornecedorAsync();
    Task<Fornecedor> GetByIdAsync(int? id);
    Task<Fornecedor> CreateAsync(Fornecedor fornecedor);
    Task<Fornecedor> UpdateAsync(Fornecedor fornecedor);
    Task<Fornecedor> DeleteAsync(Fornecedor fornecedor);
    Task EnsureConnectionOpenAsync();
}
