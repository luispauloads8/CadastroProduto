using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ClienteProjeto.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private ApplicationDbContext _categoryContext;

    public CategoriaRepository(ApplicationDbContext categoryContext)
    {
        _categoryContext = categoryContext;
    }

    public Task<Categoria> CreateAsync(Categoria categoria)
    {
        throw new NotImplementedException();
    }

    public Task<Categoria> DeleteAsync(Categoria categoria)
    {
        throw new NotImplementedException();
    }

    public async Task<Categoria> GetByIdAsync(int? id)
    {
        var categoria = await _categoryContext.Categorias.FindAsync(id);
        return categoria;
    }

    public async Task<IEnumerable<Categoria>> GetCategoriaAsync()
    {
        var categorias = await _categoryContext.Categorias.ToListAsync();
        return categorias;
    }

    public Task<Categoria> UpdateAsync(Categoria categoria)
    {
        throw new NotImplementedException();
    }
}
