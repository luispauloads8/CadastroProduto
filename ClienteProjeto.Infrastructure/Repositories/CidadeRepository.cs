using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Infrastructure.Repositories;

public class CidadeRepository : ICidadeRepository
{
    public Task<Cidade> CreateAsync(Cidade cidade)
    {
        throw new NotImplementedException();
    }

    public Task<Cidade> DeleteAsync(Cidade cidade)
    {
        throw new NotImplementedException();
    }

    public Task<Cidade> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Cidade>> GetCidadeAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Cidade> UpdateAsync(Cidade cidade)
    {
        throw new NotImplementedException();
    }
}
