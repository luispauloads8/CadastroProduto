using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface IContaContabilService
{
    Task<IEnumerable<ContaContabilDTO>> GetContaContabeis();
    Task<ContaContabilDTO> GetById(int? id);
    Task<List<ContaContabilDTO>> GetContaContabilTermo(string search);
    Task Add(ContaContabilDTO contaContabilDTO);
    Task Update(ContaContabilDTO contaContabilDTO);
    Task Delete(int? id);
}
