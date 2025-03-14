﻿using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categoria = await _categoriaService.GetCategorias();
            return Ok(categoria);
        }

        [HttpGet("{id:int}", Name = "GetCategorias")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoriaId = await _categoriaService.GetById(id);

            if (categoriaId == null)
            {
                return NoContent();
            }

            return Ok(categoriaId);
        }

        [HttpGet("{search}", Name = "GetCategoriasTermo")]
        public async Task<ActionResult<List<CategoriaDTO>>> Get(string search)
        {
            var categorias = await _categoriaService.GetCategoriasTermo(search);

            if (categorias == null || categorias.Count == 0)
            {
                return NotFound("Nenhuma categoria encontrada.");
            }

            return Ok(categorias);
        }


        [HttpPost]
        public async Task<ActionResult> Post(CategoriaDTO categoriaDTO)
        {

            if (categoriaDTO == null)
            {
                return NotFound();
            }

            await _categoriaService.Add(categoriaDTO);

            return new CreatedAtRouteResult("GetCategorias",
                  new { id = categoriaDTO.Id }, categoriaDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> Put(int id, CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO.Id != id)
            {
                return NotFound();
            }

            await _categoriaService.Update(categoriaDTO);
            return Ok(categoriaDTO);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy ="AdminOnly")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var categoria = await _categoriaService.GetById(id);
            if (categoria == null)
            {
                return NoContent();
            }
            await _categoriaService.Delete(id);
            return Ok(categoria);
        }
    }
}
