using Relatorio.Dto.Modelos;
using Relatorio.Dto.ViewModel.Lancamento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Relatorio.Dto.ViewModel
{
    [Serializable]
    public class ParametroEmissaoLancamentosVM : ParametrosDeEmissaoBaseDR
    {
        [JsonPropertyName("empresaId")]
        public int EmpresaId { get; set; }

        [JsonPropertyName("contaContabilId")]
        public int ContaContabilId { get; set; }

        [JsonPropertyName("produtoServicoId")]
        public int ProdutoServicoId { get; set; }

        [JsonPropertyName("clienteId")]
        public int ClienteId { get; set; }

        [Description("Vencimento início")]
        public DateTime? VencimentoInicio { get; set; }

        [Description("Vencimento fim")]
        public DateTime? VencimentoFim { get; set; }

        [Description("Pagamento início")]
        public DateTime? PagamentoInicio { get; set; }

        [Description("Pagamento fim")]
        public DateTime? PagamentoFim { get; set; }

        [Description("Lançamento início")]
        [JsonPropertyName("lancamentoInicio")]
        public DateTime? LancamentoInicio { get; set; }

        [Description("Lançamento fim")]
        [JsonPropertyName("lancamentoFim")]
        public DateTime? LancamentoFim { get; set; }

        [Description("Vencimento/Pagamento início")]
        public DateTime? VencimentoPagamentoInicio { get; set; }

        [Description("Vencimento/Pagamento fim")]
        public DateTime? VencimentoPagamentoFim { get; set; }

        public DtoContaContabil ContaContabil { get; set; }

        public DtoEmpresa Empresa { get; set; }

        public DtoCliente Cliente { get; set; }

        public DtoProdutoServico ProdutoServico { get; set; }

        public ICollection<DtoItensLancamento> ItensLancamentos { get; set; }

        [Description("Empresa")]
        public List<DtoEmpresa> Empresas { get; set; } = new List<DtoEmpresa>();

        public List<LancamentoVM> LancamentoVM { get; set; }

        public List<DtoLancamento> Lancamentos { get; set; }
    }
}
