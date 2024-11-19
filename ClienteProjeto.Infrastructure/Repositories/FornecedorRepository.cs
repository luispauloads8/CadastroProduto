using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Infrastructure.Repositories;

public class FornecedorRepository : IFornecedorRepository
{
    public Task<Fornecedor> CreateAsync(Fornecedor fornecedor)
    {
        throw new NotImplementedException();
    }

    public Task<Fornecedor> DeleteAsync(Fornecedor fornecedor)
    {
        throw new NotImplementedException();
    }

    public Task<Fornecedor> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Fornecedor>> GetFornecedorAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Fornecedor> UpdateAsync(Fornecedor fornecedor)
    {
        throw new NotImplementedException();
    }
}
