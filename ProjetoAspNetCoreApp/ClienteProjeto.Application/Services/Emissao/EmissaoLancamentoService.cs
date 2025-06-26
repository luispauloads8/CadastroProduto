using AutoMapper;
using ClienteProjeto.Application.DTOs;
using ClienteProjeto.Application.DTOs.Filtro;
using ClienteProjeto.Application.Interfaces.Emissao;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Interfaces;
using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Services.Emissao
{
    public class EmissaoLancamentoService : IEmissaoLancamentoService
    {
        private ILancamentoRepository _lancamentoRepository;
        private readonly IMapper _mapper;

        public EmissaoLancamentoService(ILancamentoRepository lancamentoRepository, IMapper mapper)
        {
            _lancamentoRepository = lancamentoRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LancamentoDTO>> GetLancamentos(DtoFiltroLancamento dto)
        {
            var repository = await _lancamentoRepository.GetLancamentoEmissaoAsync(dto.EmpresaId, dto.ContaContabilId, dto.LancamentoInicio, dto.LancamentoFim);

            return _mapper.Map<IEnumerable<LancamentoDTO>>(repository);
        }
    }
}
