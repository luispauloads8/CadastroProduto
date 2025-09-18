using Relatorio.Dto.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.Services.Relatorio
{
    public class ChamadaRelatorioLancamentosService : ChamadaRelatorioServiceAbstrato<ParametroEmissaoLancamentosVM>
    {
        public ChamadaRelatorioLancamentosService(HttpClient httpClient) : base(httpClient)
        {
        }

        protected override async Task AjusteParametros(ParametroEmissaoLancamentosVM entidade)
        {
            await base.AjusteParametros(entidade);
        }

        protected override string ObtenhaMontador(ParametroEmissaoLancamentosVM entidade)
        {
            return "MontadorEmissaoLancamentos";
        }
    }
}
