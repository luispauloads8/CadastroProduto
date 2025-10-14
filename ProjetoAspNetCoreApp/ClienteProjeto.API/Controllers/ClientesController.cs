using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                return NoContent();
            }
            return Ok(cliente);

        }

        [HttpGet("{search}", Name ="GetClienteTermo")]
        public async Task<ActionResult<List<ClienteDTO>>> Get(string search)
        {
            var cliente = await _clienteService.GetClienteTermo(search);

            if (cliente == null || cliente.Count == 0)
            {
                return NoContent();
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
            try
            {
                var cliente = await _clienteService.GetById(id);
                if (cliente is null)
                {
                    return NoContent();
                }

                await _clienteService.Delete(id);
                return Ok(cliente);

            }
            catch (DbUpdateException ex)
            {

                // Aqui você pode verificar se é erro de FK
                if (ex.InnerException is MySqlConnector.MySqlException mysqlEx &&
                    mysqlEx.Message.Contains("Cannot delete or update a parent row"))
                {
                    return BadRequest(new { message = "Não é possível excluir este cliente, pois existem pedidos associados a ele." });

                }

                // fallback genérico
                return StatusCode(500, "Ocorreu um erro ao excluir o cliente.");
            }

        }
    }
}
