using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
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
        public async Task<ActionResult<IEnumerable<ProdutoServico>>> Get() 
        {
            var produtos = await _produtoServicoService.GetProdutosServicos();
            return Ok(produtos);
        }
    }
}
