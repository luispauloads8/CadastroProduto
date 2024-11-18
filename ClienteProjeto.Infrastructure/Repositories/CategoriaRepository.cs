using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    public Task<Categoria> CreateAsync(Categoria categoria)
    {
        throw new NotImplementedException();
    }

    public Task<Categoria> DeleteAsync(Categoria categoria)
    {
        throw new NotImplementedException();
    }

    public Task<Categoria> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Categoria>> GetCategoriaAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Categoria> UpdateAsync(Categoria categoria)
    {
        throw new NotImplementedException();
    }
}
