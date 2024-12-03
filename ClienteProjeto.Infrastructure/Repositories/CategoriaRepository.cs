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

    public async Task<Categoria> CreateAsync(Categoria categoria)
    {
        _categoryContext.Add(categoria);
        await _categoryContext.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> DeleteAsync(Categoria categoria)
    {
        _categoryContext.Remove(categoria);
        await _categoryContext.SaveChangesAsync();
        return categoria;
    }

    public async Task<Categoria> GetByIdAsync(int? id)
    {
        return await _categoryContext.Categorias.Include(c => c.ProdutoServicos)
            .SingleOrDefaultAsync(p => p.Id == id);
        
    }

    public async Task<IEnumerable<Categoria>> GetCategoriaAsync()
    {
        var categorias = await _categoryContext.Categorias.AsNoTracking().ToListAsync();
        return categorias;
    }

    public async Task<Categoria> UpdateAsync(Categoria categoria)
    {
        var local = _categoryContext.Set<Categoria>().Local.FirstOrDefault(entry => entry.Id == categoria.Id);

        if (local != null)
        {
            _categoryContext.Entry(local).State = EntityState.Detached;
        }
        _categoryContext.Entry(categoria).State = EntityState.Modified;

        _categoryContext.Update(categoria);
        await _categoryContext.SaveChangesAsync();
        return categoria;
    }
}
