using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Application.Services;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Enum;
using ClienteProjeto.Validator.Entidade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

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
        public async Task<ActionResult<PessoaDTO>> Post([FromBody] PessoaDTO pessoaDTO)
        {
            if (pessoaDTO == null)
                return BadRequest("Corpo da requisição inválido.");

            // Cria o validador manualmente
            var validator = new ValidadorDeCadastroPessoa();
            var result = await validator.ValidateAsync(pessoaDTO);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Passa o VM direto para o serviço
            await _pessoaService.Add(pessoaDTO);

            return CreatedAtRoute("GetPessoa", new { id = pessoaDTO.Id }, pessoaDTO);
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] PessoaDTO pessoaDTO)
        {
            if (id != pessoaDTO.Id)
            {
                return BadRequest();
            }
            await _pessoaService.Update(pessoaDTO);
            return Ok(pessoaDTO);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PessoaDTO>> Delete(int id)
        {
            try
            {
                var pessoa = await _pessoaService.GetById(id);
                if (pessoa is null)
                {
                    return NoContent();
                }

                await _pessoaService.Delete(id);
                return Ok(pessoa);
            }
            catch (DbUpdateException ex)
            {

                // Aqui você pode verificar se é erro de FK
                if (ex.InnerException is MySqlConnector.MySqlException mysqlEx &&
                    mysqlEx.Message.Contains("Cannot delete or update a parent row"))
                {
                    return BadRequest(new { message = "Não é possível excluir esta pessoa, pois existe associação com cliente." });

                }

                // fallback genérico
                return StatusCode(500, "Ocorreu um erro ao excluir o cliente.");
            }

        }
    }
}
