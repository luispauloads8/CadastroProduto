using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaDTO>>> Get()
        {
            var pessoas = await _pessoaService.GetPessoas();
            return Ok(pessoas);
        }

        [HttpGet("{id:int}", Name ="GetPessoa")]
        public async Task<ActionResult<IEnumerable<PessoaDTO>>> Get(int id)
        {
            var pessoa = await _pessoaService.GetById(id);

            if(pessoa is null)
            {
                return NoContent();
            }
            return Ok(pessoa);
        }

        [HttpGet("{search}", Name = "GetPessoaTermo")]
        public async Task<ActionResult<List<PessoaDTO>>> Get(string search)
        {
            var pessoa = await _pessoaService.GetPessoaTermo(search);

            if (pessoa == null || pessoa.Count == 0)
            {
                return NoContent();
            }

            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<ActionResult<PessoaDTO>> Post(PessoaDTO pessoaDTO)
        {
            if (pessoaDTO is null)
            {
                return BadRequest();
            }
            await _pessoaService.Add(pessoaDTO);

            return new CreatedAtRouteResult("GetPessoa",
                new { id = pessoaDTO.Id }, pessoaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, PessoaDTO pessoaDTO)
        {
            if (id != pessoaDTO.Id)
            {
                return BadRequest();
            }
            await _pessoaService.Update(pessoaDTO);
            return Ok(pessoaDTO);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ClienteDTO>> Delete(int id)
        {
            var pessoa = await _pessoaService.GetById(id);
            if (pessoa is null)
            {
                return NoContent();
            }

            await _pessoaService.Delete(id);
            return Ok(pessoa);
        }
    }
}
