using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface IGrupoContaRepository
{
    Task<IEnumerable<GrupoConta>> GetGrupoContaAsync();
    Task<GrupoConta> GetByIdAsync(int? id);
    Task<GrupoConta> CreateAsync(GrupoConta grupoConta);
    Task<GrupoConta> UpdateAsync(GrupoConta grupoConta);
    Task<GrupoConta> DeleteAsync(GrupoConta grupoConta);
    Task EnsureConnectionOpenAsync();
}
