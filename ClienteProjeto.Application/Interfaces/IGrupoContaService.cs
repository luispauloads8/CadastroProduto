using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface IGrupoContaService
{
    Task<IEnumerable<GrupoContaDTO>> GetGrupoContas();
    Task<GrupoContaDTO> GetByID(int? id);
    Task Add(GrupoContaDTO grupoContaDTO);
    Task Update(GrupoContaDTO grupoContaDTO);
    Task Delete(int? id);
}
