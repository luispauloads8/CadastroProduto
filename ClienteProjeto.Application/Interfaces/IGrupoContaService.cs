using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface IGrupoContaService
{
    Task<IEnumerable<GrupoContaDTO>> GetGrupoContas();
    Task<GrupoContaDTO> GetByID(int? id);
    Task Add(GrupoContaDTO dto);
    Task Update(GrupoContaDTO dto);
    Task Delete(int? id);
    Task EnsureConnectionOpenAsync();
}
