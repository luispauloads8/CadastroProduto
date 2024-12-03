using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClienteProjeto.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoServicoService _produtoServicoService;

    public ProdutosController(IProdutoServicoService produtoServicoService)
    {
        _produtoServicoService = produtoServicoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoServicoDTO>>> Get()
    {
        var produtos = await _produtoServicoService.GetProdutosServicos();
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "GetProdutosServicos")]
    public async Task<ActionResult<ProdutoServico>> Get(int id)
    {
        await _produtoServicoService.EnsureConnectionOpenAsync();
        var produto = await _produtoServicoService.GetById(id);

        if (produto == null)
        {
            return NotFound("Produto não encontrado");
        }

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult> Post(ProdutoServicoDTO produtoServicoDTO)
    {
        if (produtoServicoDTO is null)
        {
            return BadRequest();
        }

        await _produtoServicoService.EnsureConnectionOpenAsync();
        await _produtoServicoService.Add(produtoServicoDTO);

        return new CreatedAtRouteResult("GetProdutosServicos",
            new { id = produtoServicoDTO.Id }, produtoServicoDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProdutoServicoDTO produtoServicoDTO)
    {
        await _produtoServicoService.EnsureConnectionOpenAsync();
        if (id != produtoServicoDTO.Id)
        {
            return BadRequest();
        }

        await _produtoServicoService.Update(produtoServicoDTO);

        return Ok(produtoServicoDTO);

    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoServicoDTO>> Delete(int id)
    {
        await _produtoServicoService.EnsureConnectionOpenAsync();
        var produto = await _produtoServicoService.GetById(id);
        if (produto == null)
        {
            return NotFound();
        }
        await _produtoServicoService.Delete(id);
        return Ok(produto);
    }
}
