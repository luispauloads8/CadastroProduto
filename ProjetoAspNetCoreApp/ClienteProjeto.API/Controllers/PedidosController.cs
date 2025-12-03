using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [HttpGet]
        //[Authorize(Policy="UserOnly")]
        public async Task<ActionResult<IEnumerable<PedidoDTO>>> Get()
        {
            var pedidos = await _pedidoService.GetPedidos();
            return Ok(pedidos);
        }

        [HttpGet("{id:int}", Name = "GetPedido")]
        public async Task<ActionResult<PedidoDTO>> Get(int id)
        {
            var pedido = await _pedidoService.GetById(id);

            if (pedido == null)
            {
                return NoContent();
            }

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult<PedidoDTO>> Post([FromBody] PedidoDTO pedidoDTO)
        {
            if (pedidoDTO == null)
            {
                return BadRequest();
            }
            await _pedidoService.Add(pedidoDTO);

            return new CreatedAtRouteResult("GetPedido",
                new { id = pedidoDTO.Id }, pedidoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PedidoDTO>> Put(int id, PedidoDTO pedidoDTO)
        {
            if (pedidoDTO.Id != id)
            {
                return NotFound();
            }

            await _pedidoService.Update(pedidoDTO);
            return Ok(pedidoDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PedidoDTO>> Delete(int id)
        {
            var produto = await _pedidoService.GetById(id);
            if (produto == null)
            {
                return NotFound();
            }
            await _pedidoService.Delete(id);
            return Ok(produto);
        }

        [HttpGet("paginado")]
        public async Task<ActionResult<PagedResult<PedidoDTO>>> GetPaginado(
            int pageNumber = 1,
            int pageSize = 10)
        {
            var resultado = await _pedidoService.ObterPaginadoAsync(pageNumber, pageSize);
            return Ok(resultado);
        }

    }
}
