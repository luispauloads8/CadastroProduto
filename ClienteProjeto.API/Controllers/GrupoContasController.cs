using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Http;
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
    }
}
