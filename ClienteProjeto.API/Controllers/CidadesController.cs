using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadesController : ControllerBase
    {
        private readonly ICidadeService _cidadeService;

        public CidadesController(ICidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CidadeDTO>>> Get()
        {
            var cidades = await _cidadeService.GetCidades();
            return Ok(cidades);
        }

        [HttpGet("{id:int}", Name = "GetCidade")]
        public async Task<ActionResult<CidadeDTO>> Get(int id)
        {
            var cidade = await _cidadeService.GetById(id);

            if (cidade == null)
            {
                return NotFound("Cidade não encontrada");
            }
            return Ok(cidade);

        }

        [HttpPost]
        public async Task<ActionResult<CidadeDTO>> Post(CidadeDTO cidadeDTO)
        {
            if (cidadeDTO is null)
            {
                return BadRequest();
            }

            await _cidadeService.Add(cidadeDTO);

            return new CreatedAtRouteResult("GetCidade",
                new { id = cidadeDTO.Id }, cidadeDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CidadeDTO cidadeDTO)
        {
            if (id != cidadeDTO.Id)
            {
                return BadRequest();
            }

            await _cidadeService.Update(cidadeDTO);

            return Ok(cidadeDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CidadeDTO>> Delete(int id)
        {
            var cidade = await _cidadeService.GetById(id);
            if (cidade == null)
            {
                return NotFound();
            }
            await _cidadeService.Delete(id);
            return Ok(cidade);

        } 
    }
}
