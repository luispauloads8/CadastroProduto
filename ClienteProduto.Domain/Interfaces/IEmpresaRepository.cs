using ClienteProjeto.Domain.Entities;

namespace ClienteProjeto.Domain.Interfaces;

public interface IEmpresaRepository
{
    Task<IEnumerable<Empresa>> GetEmpresaAsync();
    Task<Empresa> GetByIdAsync(int? id);
    Task<Empresa> CreateAsync(Empresa empresa);
    Task<Empresa> UpdateAsync(Empresa empresa);
    Task<Empresa> DeleteAsync(Empresa empresa);
    Task EnsureConnectionOpenAsync();
}
