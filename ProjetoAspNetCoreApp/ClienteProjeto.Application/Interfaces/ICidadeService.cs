using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface ICidadeService
{
    Task<IEnumerable<CidadeDTO>> GetCidades();
    Task<List<CidadeDTO>> GetCidadeTermo(string termo);
    Task<CidadeDTO> GetById(int? id);
    Task Add(CidadeDTO cidadeDTO);
    Task Update(CidadeDTO cidadeDTO);
    Task Delete(int? id);
}
