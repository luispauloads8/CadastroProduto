using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaContabeisController : ControllerBase
    {
        private readonly IContaContabilService _contaContabilService;

        public ContaContabeisController(IContaContabilService contaContabilService)
        {
            _contaContabilService = contaContabilService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContaContabilDTO>>> Get()
        {
            var contasContabeis = await _contaContabilService.GetContaContabeis();
            return Ok(contasContabeis);
        }

        [HttpGet("{id:int}", Name ="GetContaContabil")]
        public async Task<ActionResult<ContaContabilDTO>> Get(int id)
        {
            var contaContabil = await _contaContabilService.GetById(id);

            if (contaContabil == null) 
            {
                return NotFound("Conta Contabil não encontrada");
            }

            return Ok(contaContabil);
        }

        [HttpPost]
        public async Task<ActionResult<ContaContabilDTO>> Post(ContaContabilDTO contaContabilDTO)
        {
            if (contaContabilDTO == null)
            {
                return BadRequest();
            }
           await _contaContabilService.Add(contaContabilDTO);

            return new CreatedAtRouteResult("GetContaContabil",
                new { id = contaContabilDTO.Id }, contaContabilDTO);
        }

        [HttpPut]
        public async Task<ActionResult>  Put(int id, ContaContabilDTO contaContabilDTO)
        {
            if (contaContabilDTO.Id != id)
            {
                return BadRequest();
            }
            await _contaContabilService.Update(contaContabilDTO);

            return Ok(contaContabilDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ContaContabilDTO>> Delete(int id)
        {
            var contaContabil = _contaContabilService.GetById(id);
            if (contaContabil == null)
            {
                return NotFound("Conta não encontrada");
            }
            await _contaContabilService.Delete(id);
            return Ok(contaContabil);
        }
    }
}
