using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface ICatalogoRepository
{
    Task<IEnumerable<Categoria>> GetCategoriaAsync();
    Task<Categoria> GetByIdAsync(int? id);
    Task<Categoria> CreateAsync(Categoria categoria);
    Task<Categoria> UpdateAsync(Categoria categoria);
    Task<Categoria> DeleteAsync(Categoria categoria);

}
