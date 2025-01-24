using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetCategoriaAsync();
    Task<List<Categoria>> GetCategoriaTermoAsync(string search);
    Task<Categoria> GetByIdAsync(int? id);
    Task<Categoria> CreateAsync(Categoria categoria);
    Task<Categoria> UpdateAsync(Categoria categoria);
    Task<Categoria> DeleteAsync(Categoria categoria);
    Task EnsureConnectionOpenAsync();
}
