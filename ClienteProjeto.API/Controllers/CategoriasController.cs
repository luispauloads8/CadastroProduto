using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClienteProjeto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            await _categoriaService.EnsureConnectionOpenAsync();
            var categoria = await _categoriaService.GetCategorias();
            return Ok(categoria);
        }

        [HttpGet("{id:int}", Name = "GetCategorias")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            await _categoriaService.EnsureConnectionOpenAsync();
            var categoriaId = await _categoriaService.GetById(id);

            if (categoriaId == null)
            {
                return NotFound("Categoria não encontrada");
            }

            return Ok(categoriaId);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoriaDTO categoriaDTO)
        {

            if (categoriaDTO == null)
            {
                return NotFound();
            }

            await _categoriaService.EnsureConnectionOpenAsync();
            await _categoriaService.Add(categoriaDTO);

            return new CreatedAtRouteResult("GetCategorias",
                  new { id = categoriaDTO.Id }, categoriaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDTO)
        {
            await _categoriaService.EnsureConnectionOpenAsync();
            if (categoriaDTO.Id != id)
            {
                return NotFound();
            }

            await _categoriaService.Update(categoriaDTO);
            return Ok(categoriaDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            await _categoriaService.EnsureConnectionOpenAsync();
            var categoria = await _categoriaService.GetById(id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }
            await _categoriaService.Delete(id);
            return Ok(categoria);
        }
    }
}
