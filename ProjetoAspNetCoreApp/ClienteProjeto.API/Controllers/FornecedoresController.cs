using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedoresController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> Get()
        {
            var fornecedores = await _fornecedorService.GetFornecedores();
            return Ok(fornecedores);
        }

        [HttpGet("{id:int}", Name = "GetFornecedor")]
        public async Task<ActionResult<FornecedorDTO>> Get(int id)
        {
            var fornecedor = await _fornecedorService.GetById(id);
            if (fornecedor == null)
            {
                return NoContent();
            }
            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorDTO>> Post(FornecedorDTO fornecedorDTO)
        {
            if (fornecedorDTO is null)
            {
                return BadRequest();
            }
            await _fornecedorService.Add(fornecedorDTO);

            return new CreatedAtRouteResult("GetFornecedor",
                new { id = fornecedorDTO.Id }, fornecedorDTO);
        }

        [HttpPut("{id:int}")]
        public async  Task<ActionResult> Put(int id, FornecedorDTO fornecedorDTO)
        {
            if (fornecedorDTO.Id != id)
            {
                return BadRequest();
            }
            await _fornecedorService.Update(fornecedorDTO);

            return Ok(fornecedorDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<FornecedorDTO>> Delete(int id)
        {
            var fornecedor = await _fornecedorService.GetById(id);

            if (fornecedor is null)
            {
                return NoContent();
            }
            await _fornecedorService.Delete(id);
            return Ok(fornecedor);
        }
    }
}
