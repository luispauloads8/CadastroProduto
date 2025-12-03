using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface ILancamentoRepository
{
    Task<IEnumerable<Lancamento>> GetLancamentoAsync();
    Task<IEnumerable<Lancamento>> GetLancamentoEmissaoAsync(
        int? empresaId,
        int? contaContabilId,
        DateTime? lancamentoInicio,
        DateTime? lancamentoFim);
    Task<Lancamento> GetByIdAsync(int? id);
    Task<Lancamento> CreateAsync(Lancamento lancamento);
    Task<Lancamento> UpdateAsync(Lancamento lancamento);
    Task<Lancamento> DeleteAsync(Lancamento lancamento);
    Task<PagedResult<Lancamento>> GetPagedAsync(int pageNumber, int pageSize);
    Task EnsureConnectionOpenAsync();
}
