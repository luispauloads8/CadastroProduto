using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ClienteProjeto.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ApplicationDbContext _categoryContext;

    public CategoriaRepository(ApplicationDbContext categoryContext, IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _categoryContext = categoryContext;
        _contextFactory = contextFactory;
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

    public async Task EnsureConnectionOpenAsync()
    {
        var context = _contextFactory.CreateDbContext();
        var connection = context.Database.GetDbConnection();
        Console.WriteLine("Estado atual da conexão: " + connection.State);
        if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
        {
            Console.WriteLine("Tentando abrir a conexão...");
            await connection.OpenAsync();
            Console.WriteLine("Conexão aberta.");
        }
        else if (connection.State == ConnectionState.Connecting)
        {
            Console.WriteLine("A conexão já está em processo de abertura.");
        }
    }

    public async Task<List<Categoria>> GetCategoriaTermoAsync(string search)
    {
        return await _categoryContext.Categorias
         .Where(c => c.Descricao.Contains(search))
         .ToListAsync();
    }
}
