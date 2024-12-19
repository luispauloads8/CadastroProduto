using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface IEmpresaService
{
    Task<IEnumerable<EmpresaDTO>> GetEmpresas();
    Task<EmpresaDTO> GetById(int? id);
    Task Add(EmpresaDTO empresaDTO);
    Task Update(EmpresaDTO empresaDTO);
    Task Delete(int? id);
}
