using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Infrastructure.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    public Task<Empresa> CreateAsync(Empresa empresa)
    {
        throw new NotImplementedException();
    }

    public Task<Empresa> DeleteAsync(Empresa empresa)
    {
        throw new NotImplementedException();
    }

    public Task<Empresa> GetByIdAsync(int? id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Empresa>> GetEmpresaAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Empresa> UpdateAsync(Empresa empresa)
    {
        throw new NotImplementedException();
    }
}
