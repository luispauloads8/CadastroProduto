using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface ICidadeRepository
{
    Task<IEnumerable<Cidade>> GetCidadeAsync();
    Task<Cidade> GetByIdAsync(int? id);
    Task<Cidade> CreateAsync(Cidade cidade);
    Task<Cidade> UpdateAsync(Cidade cidade);
    Task<Cidade> DeleteAsync(Cidade cidade);
}
