using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data;

namespace ClienteProjeto.Application.Services;

public class CategoriaService : ICategoriaService
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper,IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
    {
        var categorias = await _categoriaRepository.GetCategoriaAsync();
        return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
    }

    public async Task<CategoriaDTO> GetById(int? id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        return _mapper.Map<CategoriaDTO>(categoria);
    }

    public async Task Add(CategoriaDTO categoriaDTO)
    {
        var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);
        await _categoriaRepository.CreateAsync(categoriaEntity);
    }

    public async Task Delete(int? id)
    {
        var categoriaEntity = _categoriaRepository.GetByIdAsync(id).Result;
        await _categoriaRepository.DeleteAsync(categoriaEntity);
    }

    public async Task Update(CategoriaDTO categoriaDTO)
    {
        var categoriaEntity = _mapper.Map<Categoria>(categoriaDTO);
        await _categoriaRepository.UpdateAsync(categoriaEntity);
    }

    public async Task EnsureConnectionOpenAsync()
    {
        var context = _contextFactory.CreateDbContext();
        var connection = context.Database.GetDbConnection();
        Console.WriteLine("Estado atual da conexão: " + connection.State);
        if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
        {
            Console.WriteLine("Tentando abrir a conexão...");
            await connection.OpenAsync();
            Console.WriteLine("Conexão aberta.");
        }
        else if (connection.State == ConnectionState.Connecting)
        {
            Console.WriteLine("A conexão já está em processo de abertura.");
        }
    }
}
