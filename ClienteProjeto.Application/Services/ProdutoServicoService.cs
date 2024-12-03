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

public class ProdutoServicoService : IProdutoServicoService
{
    private IDbContextFactory<ApplicationDbContext> _contextFactory;
    private IProdutoServicoRepository _produtoServicoRepository;
    private readonly IMapper _mapper;

    public ProdutoServicoService(IProdutoServicoRepository produtoServicoRepository, IMapper mapper, IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _produtoServicoRepository = produtoServicoRepository ??
            throw new ArgumentNullException(nameof(produtoServicoRepository));
        _mapper = mapper;
        _contextFactory = contextFactory;
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

    public async Task Add(ProdutoServicoDTO produtoServicoDTO)
    {
        var produtoEntiy =  _mapper.Map<ProdutoServico>(produtoServicoDTO);
        await _produtoServicoRepository.CreateAsync(produtoEntiy);
    }

    public async Task Delete(int? id)
    {
        var produto = _produtoServicoRepository.GetByIdAsync(id).Result;
        await _produtoServicoRepository.DeleteAsync(produto);
    }

    public async Task<ProdutoServicoDTO> GetById(int? id)
    {   
        var produto = await _produtoServicoRepository.GetByIdAsync(id);
        return _mapper.Map<ProdutoServicoDTO>(produto);
    }

    public async Task<IEnumerable<ProdutoServicoDTO>> GetProdutosServicos()
    {
        var produtoEntity = await _produtoServicoRepository.GetProdutoServicoAsync();
        return _mapper.Map<IEnumerable<ProdutoServicoDTO>>(produtoEntity);
    }

    public async Task Update(ProdutoServicoDTO produtoDTO)
    {
        var produtoEntity = _mapper.Map<ProdutoServico>(produtoDTO);
        await _produtoServicoRepository.UpdateAsync(produtoEntity);
    }
}
