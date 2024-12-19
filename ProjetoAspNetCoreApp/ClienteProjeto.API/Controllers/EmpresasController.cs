using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaDTO>>> Get()
        {
            var empresas = await _empresaService.GetEmpresas();
            return Ok(empresas);
        }

        [HttpGet("{id:int}", Name ="GetEmpresa")]
        public async Task<ActionResult<EmpresaDTO>> Get(int id)
        {
            var empresa = await _empresaService.GetById(id);

            if (empresa is null)
            {
                return NotFound("Empresa não Encontrada");
            }

            return Ok(empresa);
        }

        [HttpPost]
        public async Task<ActionResult<EmpresaDTO>> Post(EmpresaDTO empresaDTO)
        {
            if (empresaDTO is null)
            {
                return BadRequest();
            }
            await _empresaService.Add(empresaDTO);

            return new CreatedAtRouteResult("GetEmpresa",
                new { id = empresaDTO.Id }, empresaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,  EmpresaDTO empresaDTO)
        {
            if (id != empresaDTO.Id)
            {
                return BadRequest();
            }
            await _empresaService.Update(empresaDTO);
            return Ok(empresaDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<EmpresaDTO>> Delete(int id)
        {
            var empresa = await _empresaService.GetById(id);

            if (empresa is null)
            {
                return NotFound("Empresa não Encontrada");
            }

            await _empresaService.Delete(id); 
            return Ok(empresa);

        }

    }
}
