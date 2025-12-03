using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Application.Services;
using ClienteProjeto.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(Policy="UserOnly")]
    public async Task<ActionResult<IEnumerable<ProdutoServicoDTO>>> Get()
    {
        var produtos = await _produtoServicoService.GetProdutosServicos();
        return Ok(produtos);
    }

    [HttpGet("{id:int}", Name = "GetProdutosServico")]
    public async Task<ActionResult<ProdutoServicoDTO>> Get(int id)
    {
        var produto = await _produtoServicoService.GetById(id);

        if (produto == null)
        {
            return NoContent();
        }

        return Ok(produto);
    }

    [HttpGet("{search}", Name ="GetProdutoServicoTermo")]
    public async Task<ActionResult<List<ProdutoServicoDTO>>> Get(string search)
    {
        var produtosServicos = await _produtoServicoService.GetProdutoServicoTermo(search);

        if (produtosServicos == null || produtosServicos.Count == 0) 
        {
            return NoContent();
        }

        return Ok(produtosServicos);
    }


    [HttpPost]
    public async Task<ActionResult> Post(ProdutoServicoDTO produtoServicoDTO)
    {
        if (produtoServicoDTO is null)
        {
            return BadRequest();
        }

        await _produtoServicoService.Add(produtoServicoDTO);

        return new CreatedAtRouteResult("GetProdutosServico",
            new { id = produtoServicoDTO.Id }, produtoServicoDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, [FromBody] ProdutoServicoDTO produtoServicoDTO)
    {
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

        try
        {
            var produto = await _produtoServicoService.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }
            await _produtoServicoService.Delete(id);
            return Ok(produto);
        }
        catch (DbUpdateException ex)
        {

            // Aqui você pode verificar se é erro de FK
            if (ex.InnerException is MySqlConnector.MySqlException mysqlEx &&
                mysqlEx.Message.Contains("Cannot delete or update a parent row"))
            {
                return BadRequest(new { message = "Não é possível excluir este produto/serviço, pois existe associação" });

            }

            // fallback genérico
            return StatusCode(500, "Ocorreu um erro ao excluir o cliente.");
        }


    }

    [HttpGet("paginado")]
    public async Task<ActionResult<PagedResult<ProdutoServicoDTO>>> GetPaginado(
        int pageNumber = 1,
        int pageSize = 10)
    {
        var resultado = await _produtoServicoService.ObterPaginadoAsync(pageNumber, pageSize);
        return Ok(resultado);
    }
}
