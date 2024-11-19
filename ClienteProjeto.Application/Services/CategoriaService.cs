using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Interfaces;

namespace ClienteProjeto.Application.Services;

public class CategoriaService : ICategoriaService
{
    private ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
    {
        var categorias = await _categoriaRepository.GetCategoriaAsync();
        return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
    }

    public Task<CategoriaDTO> GetById(int? id)
    {
        throw new NotImplementedException();
    }

    public Task Add(CategoriaDTO categoriaDTO)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int? id)
    {
        throw new NotImplementedException();
    }

    public Task Update(CategoriaDTO categoriaDTO)
    {
        throw new NotImplementedException();
    }
}
