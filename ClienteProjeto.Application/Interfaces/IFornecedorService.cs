using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface IFornecedorService
{
    Task<IEnumerable<FornecedorDTO>> GetFornecedores();
    Task<FornecedorDTO> GetById(int? id);
    Task Add(FornecedorDTO fornecedorDTO);
    Task Update(FornecedorDTO fornecedorDTO);
    Task Delete(int? id);
    Task EnsureConnectionOpenAsync();
}
