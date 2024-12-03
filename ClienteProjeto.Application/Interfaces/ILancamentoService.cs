using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface ILancamentoService
{
    Task<IEnumerable<LancamentoDTO>> GetLancamentos();
    Task<LancamentoDTO> GetById(int? id);
    Task Add(LancamentoDTO lancamentoDTO);
    Task Update(LancamentoDTO lancamentoDTO);
    Task Delete(int? id);
    Task EnsureConnectionOpenAsync();
}
