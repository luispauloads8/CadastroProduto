using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using SixLabors.ImageSharp.Formats;

using SixLabors.ImageSharp;
using Image = SixLabors.ImageSharp.Image;
using Microsoft.AspNetCore.Identity;


namespace ClienteProjeto.Application.Services;

public class ProdutoServicoService : IProdutoServicoService
{
    private IProdutoServicoRepository _produtoServicoRepository;
    private readonly IMapper _mapper;

    public ProdutoServicoService(IProdutoServicoRepository produtoServicoRepository, IMapper mapper)
    {
        _produtoServicoRepository = produtoServicoRepository ??
            throw new ArgumentNullException(nameof(produtoServicoRepository));
        _mapper = mapper;
    }

    public async Task Add(ProdutoServicoDTO produtoServicoDTO)
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();

        ConverteImagemBase64(produtoServicoDTO);

         var produtoEntiy = _mapper.Map<ProdutoServico>(produtoServicoDTO);

        await _produtoServicoRepository.CreateAsync(produtoEntiy);
    }

    public async Task Delete(int? id)
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();
        var produto = await _produtoServicoRepository.GetByIdAsync(id);
        await _produtoServicoRepository.DeleteAsync(produto);
    }

    public async Task<ProdutoServicoDTO> GetById(int? id)
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();
        var produto = await _produtoServicoRepository.GetByIdAsync(id);

        var produtodto = _mapper.Map<ProdutoServicoDTO>(produto);

        if (produto.Imagem != null)
        {
            produtodto.Imagem = Convert.ToBase64String(produto.Imagem);
        }

        return produtodto;
    }

    public async Task<IEnumerable<ProdutoServicoDTO>> GetProdutosServicos()
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();
        var produtoEntity = await _produtoServicoRepository.GetProdutoServicoAsync();

        // Converte o campo Blob para Base64
        var produtoServicosDto = produtoEntity.Select(produto =>
        {
            var produtoServicodto = _mapper.Map<ProdutoServicoDTO>(produto);
            if (produto.Imagem != null)
            {
                produtoServicodto.Imagem = Convert.ToBase64String(produto.Imagem);
            }
            return produtoServicodto;
        });

        return produtoServicosDto;
    }

    public async Task Update(ProdutoServicoDTO produtoDTO)
    {
        await _produtoServicoRepository.EnsureConnectionOpenAsync();

        ConverteImagemBase64(produtoDTO);

        var produtoEntity = _mapper.Map<ProdutoServico>(produtoDTO);
        await _produtoServicoRepository.UpdateAsync(produtoEntity);
    }

    private static void ConverteImagemBase64(ProdutoServicoDTO produtoDTO)
    {
        try
        {
            if (!string.IsNullOrEmpty(produtoDTO.Imagem))
            {
                produtoDTO.ImagemByteArray = Convert.FromBase64String(produtoDTO.Imagem);
            }
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Erro ao converter imagem: {ex.Message}");
        }
    }

    public async Task<List<ProdutoServicoDTO>> GetProdutoServicoTermo(string search)
    {
        if (string.IsNullOrWhiteSpace(search))
        {
            return new List<ProdutoServicoDTO>();
        }

        var produtoServicoTermo = await _produtoServicoRepository.GetProdutoServicoTermoAsync(search);

        return _mapper.Map<List<ProdutoServicoDTO>>(produtoServicoTermo);
    }

    private static ProdutoServicoDTO MapToDTO(ProdutoServico produto)
    {
        return new ProdutoServicoDTO
        {
            Id = produto.Id,
            Descricao = produto.Descricao,
            Preco = produto.Preco,
            ImagemByteArray = produto.Imagem,
            Imagem = produto.Imagem != null && produto.Imagem.Length > 0
                ? Convert.ToBase64String(produto.Imagem)
                : null
        };
    }

    public async Task<PagedResult<ProdutoServicoDTO>> ObterPaginadoAsync(int pageNumber, int pageSize)
    {
        // Aqui pode entrar regra de negócio, validação, logs, etc
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var produtoEntity = await _produtoServicoRepository.GetPagedAsync(pageNumber, pageSize);

        var dtoItems = produtoEntity.Items.Select(MapToDTO).ToList();
       
        return new PagedResult<ProdutoServicoDTO>
        {
            Items = dtoItems,
            PageNumber = produtoEntity.PageNumber,
            PageSize = produtoEntity.PageSize,
            TotalItems = produtoEntity.TotalItems
        };
    }
}
