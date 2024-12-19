using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentosController : ControllerBase
    {
        private readonly ILancamentoService _lancamentoService;

        public LancamentosController(ILancamentoService lancamentoService)
        {
            _lancamentoService = lancamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LancamentoDTO>>> Get()
        {
            var lancamentosDTO = await _lancamentoService.GetLancamentos();
            return Ok(lancamentosDTO);
        }

        [HttpGet("{id:int}", Name ="GetLancamento")]
        public async Task<ActionResult<LancamentoDTO>> Get(int id)
        {
            var lancamentoDTO = await _lancamentoService.GetById(id);

            if (lancamentoDTO == null)
            {
                return NotFound("Lançamento não encontrado");
            }

            return Ok(lancamentoDTO);
        }

        [HttpPost]
        public async Task<ActionResult<LancamentoDTO>> Post(LancamentoDTO lancamentoDTO)
        {
            if (lancamentoDTO == null)
            {
                return BadRequest();
            }
            await _lancamentoService.Add(lancamentoDTO);

            return new CreatedAtRouteResult("GetLancamento",
                new { id = lancamentoDTO.Id }, lancamentoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, LancamentoDTO lancamentoDTO)
        {
            if (id != lancamentoDTO.Id)
            {
                return BadRequest();
            }

            await _lancamentoService.Update(lancamentoDTO);
            return Ok(lancamentoDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<LancamentoDTO>> Delete(int id)
        {
            var lancamentoDTO = await _lancamentoService.GetById(id);
            if (lancamentoDTO == null)
            {
                return BadRequest();
            }

            await _lancamentoService.Delete(id);
            return Ok(lancamentoDTO);
        }
    }
}
