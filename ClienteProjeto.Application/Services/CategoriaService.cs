using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
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
        await _categoriaRepository.EnsureConnectionOpenAsync();
        var categorias = await _categoriaRepository.GetCategoriaAsync();
        return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
    }

    public async Task<CategoriaDTO> GetById(int? id)
    {
        await _categoriaRepository.EnsureConnectionOpenAsync();
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        return _mapper.Map<CategoriaDTO>(categoria);
    }

    public async Task Add(CategoriaDTO categoriaDTO)
    {
        await _categoriaRepository.EnsureConnectionOpenAsync();
        var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);
        await _categoriaRepository.CreateAsync(categoriaEntity);
    }

    public async Task Delete(int? id)
    {
        await _categoriaRepository.EnsureConnectionOpenAsync();
        var categoriaEntity = await _categoriaRepository.GetByIdAsync(id);
        await _categoriaRepository.DeleteAsync(categoriaEntity);
    }

    public async Task Update(CategoriaDTO categoriaDTO)
    {
        await _categoriaRepository.EnsureConnectionOpenAsync();
        var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);
        await _categoriaRepository.UpdateAsync(categoriaEntity);
    }
}
