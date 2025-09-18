using AutoMapper;
using ClienteProjeto.Application.ConversaoEntidade;
using ClienteProjeto.Application.DTOs.Filtro;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Application.Interfaces.Emissao;
using ClienteProjeto.Domain.Entities;
using Relatorio.Dto;
using Relatorio.Dto.Modelos;
using Relatorio.Dto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Services.Relatorio
{
    public class ChamadaRelatorioLancamentoService : ChamadaRelatorioServiceAbstrato<ParametroEmissaoLancamentosVM>
    {
        private IEmissaoLancamentoService _emissaoLancamentoService;
        private IMapper _mapper;

        public ChamadaRelatorioLancamentoService(HttpClient httpClient, IEmissaoLancamentoService emissaoLancamentoService, IMapper mapper) : base(httpClient)
        {
            _emissaoLancamentoService = emissaoLancamentoService;
            _mapper = mapper;
        }

        protected override async Task AjusteParametros(ParametroEmissaoLancamentosVM entidade)
        {
            await base.AjusteParametros(entidade);

            var filtro = _mapper.Map<ParametroEmissaoLancamentosVM, DtoFiltroLancamento>(entidade);

            var lancamentos = await _emissaoLancamentoService.GetLancamentos(filtro);

            entidade.Lancamentos = ServicoConversaoLancamento.Converta(lancamentos);
        }

        protected override string ObtenhaMontador(ParametroEmissaoLancamentosVM entidade)
        {
            return "MontadorDeEmissaoLancamento";
        }
    }
}
