using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface ICategoriaRepository
{
    Task<IEnumerable<Categoria>> GetCategoriaAsync();
    Task<IEnumerable<Categoria>> GetCategoriaEmissaoAsync(
        List<Categoria> categoria);
    Task<List<Categoria>> GetCategoriaTermoAsync(string search);
    Task<Categoria> GetByIdAsync(int? id);
    Task<Categoria> CreateAsync(Categoria categoria);
    Task<Categoria> UpdateAsync(Categoria categoria);
    Task<Categoria> DeleteAsync(Categoria categoria);
    Task<PagedResult<Categoria>> GetPagedAsync(int pageNumber, int pageSize, string searchTerm);
    Task EnsureConnectionOpenAsync();
}
