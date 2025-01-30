using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using SixLabors.ImageSharp.Formats;

using SixLabors.ImageSharp;
using Image = SixLabors.ImageSharp.Image;


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

}
