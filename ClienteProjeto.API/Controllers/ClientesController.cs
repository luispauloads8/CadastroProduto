using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get()
        {
            var clientes = await _clienteService.GetClientes();
            return Ok(clientes);
        }

        [HttpGet("{id:int}", Name ="GetCliente")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var cliente = await _clienteService.GetById(id);

            if (cliente is null)
            {
                return NotFound("Cliente não Encontrado");
            }
            return Ok(cliente);

        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Post(ClienteDTO clienteDTO)
        {
            if (clienteDTO is null)
            {
                return BadRequest();
            }
            await _clienteService.Add(clienteDTO);

            return new CreatedAtRouteResult("GetCliente",
                new { id = clienteDTO.Id }, clienteDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ClienteDTO clienteDTO)
        {
            if (id != clienteDTO.Id)
            {
                return BadRequest();
            }

            await _clienteService.Update(clienteDTO);
            return Ok(clienteDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> Delete(int id)
        {
            var cliente = await _clienteService.GetById(id);
            if (cliente is null)
            {
                return NotFound("Cliente não encontrado");
            }

            await _clienteService.Delete(id);
            return Ok(cliente);
        }
    }
}
