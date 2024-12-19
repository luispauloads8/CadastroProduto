using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoContasController : ControllerBase
    {
        private readonly IGrupoContaService _grupoContaService;

        public GrupoContasController(IGrupoContaService grupoContaService)
        {
            _grupoContaService = grupoContaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GrupoContaDTO>>> Get()
        {
            var grupoContas = await _grupoContaService.GetGrupoContas();
            return Ok(grupoContas);
        }

        [HttpGet("{id:int}", Name ="GetGrupoConta")]
        public async  Task<ActionResult<GrupoContaDTO>> Get(int id)
        {
            var grupoConta = await _grupoContaService.GetByID(id);
            if (grupoConta == null)
            {
                return NotFound("Grupo de Contas não encontrado");
            }
            return Ok(grupoConta);
        }

        [HttpPost]
        public async Task<ActionResult<GrupoContaDTO>> Post(GrupoContaDTO grupoContaDTO)
        {
            if (grupoContaDTO == null) 
            { 
                return BadRequest(); 
            }
            await _grupoContaService.Add(grupoContaDTO);

            return new CreatedAtRouteResult("GetGrupoConta",
                new { id = grupoContaDTO.Id }, grupoContaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, GrupoContaDTO grupoContaDTO)
        {
            if (id != grupoContaDTO.Id)
            {
                return BadRequest();
            }
            await _grupoContaService.Update(grupoContaDTO);
            return Ok(grupoContaDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<GrupoContaDTO>> Delete(int id)
        {
            var grupoContaDTO = await _grupoContaService.GetByID(id);

            if (grupoContaDTO == null)
            {
                return BadRequest();
            }

            await _grupoContaService.Delete(id);
            return Ok(grupoContaDTO);
        }
    }
}
