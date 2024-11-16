using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface IItensLancamentoService
{
    Task<IEnumerable<ItensLancamentoDTO>> GetItensLancamentos();
    Task<ItensLancamentoDTO> GetById(int? id);
    Task Add(ItensLancamentoDTO itensLancamentoDTO);
    Task Update(ItensLancamentoDTO itensLancamentoDTO);
    Task Delete(int? id);
}
