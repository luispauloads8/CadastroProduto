using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface ILancamentoRepository
{
    Task<IEnumerable<Lancamento>> GetLancamentoAsync();
    Task<Lancamento> GetByIdAsync(int? id);
    Task<Lancamento> CreateAsync(Lancamento lancamento);
    Task<Lancamento> UpdateAsync(Lancamento lancamento);
    Task<Lancamento> DeleteAsync(Lancamento lancamento);
    Task EnsureConnectionOpenAsync();
}
