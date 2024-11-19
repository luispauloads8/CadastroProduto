using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Infrastructure.Repositories;

public class GrupoContaRepository : IGrupoContaRepository
{
    public Task<GrupoConta> CreateAsync(GrupoConta grupoConta)
    {
        throw new NotImplementedException();
    }

    public Task<GrupoConta> DeleteAsync(GrupoConta grupoConta)
    {
        throw new NotImplementedException();
    }

    public Task<GrupoConta> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GrupoConta>> GetGrupoContaAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GrupoConta> UpdateAsync(GrupoConta grupoConta)
    {
        throw new NotImplementedException();
    }
}
