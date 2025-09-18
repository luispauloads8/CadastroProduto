using AutoMapper;
using ClienteProjeto.Application.ConversaoEntidade;
using ClienteProjeto.Application.DTOs.Filtro;
using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Application.Interfaces.Emissao;
using Relatorio.Dto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Services.Relatorio
{
    public class ChamadaRelatorioCategoriaService : ChamadaRelatorioServiceAbstrato<ParametroEmissaoCategoriaVM>
    {
        private IEmissaoCategoriaService _emissaoCategoriaService;
        private IMapper _mapper;

        public ChamadaRelatorioCategoriaService(HttpClient httpClient, IEmissaoCategoriaService emissaoCategoriaService, IMapper mapper) : base(httpClient)
        {
            _emissaoCategoriaService = emissaoCategoriaService;
            _mapper = mapper;
        }

        protected override async Task AjusteParametros(ParametroEmissaoCategoriaVM entidade)
        {
            await base.AjusteParametros(entidade);

            var filtro = _mapper.Map<ParametroEmissaoCategoriaVM, DtoFiltroCategoria>(entidade);

            var categorias = await _emissaoCategoriaService.GetCategorias(filtro);

            entidade.Categorias = ServicoConversaoCategoria.Conversao(categorias);
        }

        protected override string ObtenhaMontador(ParametroEmissaoCategoriaVM entidade)
        {
            return "MontadorDeEmissaoCategoria";
        }
    }
}
