using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.DTOs.Filtro;
using ClienteProjeto.Application.Interfaces.Emissao;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Services.Emissao
{
    public class EmissaoCategoriaService : IEmissaoCategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public EmissaoCategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategorias(DtoFiltroCategoria dto)
        {
            var repository = await _categoriaRepository.GetCategoriaEmissaoAsync(dto.Categoriasvm);

            return _mapper.Map<IEnumerable<CategoriaDTO>>(repository);
        }
    }
}
