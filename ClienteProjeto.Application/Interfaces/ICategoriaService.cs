using ClienteProjeto.Application.DTOs;

namespace ClienteProjeto.Application.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDTO>> GetCategorias();
    Task<CategoriaDTO> GetById(int? id);
    Task Add(CategoriaDTO categoriaDTO);
    Task Update(CategoriaDTO categoriaDTO);
    Task Delete(int ?id);
}
