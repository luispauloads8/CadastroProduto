using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Infrastructure.Repositories;

public class LancamentoRepository : ILancamentoRepository
{
    public Task<Lancamento> CreateAsync(Lancamento lancamento)
    {
        throw new NotImplementedException();
    }

    public Task<Lancamento> DeleteAsync(Lancamento lancamento)
    {
        throw new NotImplementedException();
    }

    public Task<Lancamento> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Lancamento>> GetLancamentoAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Lancamento> UpdateAsync(Lancamento lancamento)
    {
        throw new NotImplementedException();
    }
}
