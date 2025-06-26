using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs.Filtro
{
    public class DtoFiltroLancamento
    {
        public int EmpresaId { get; set; }
        public int ContaContabilId { get; set; }
        public int ProdutoServicoId { get; set; }
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
        public DateTime? LancamentoInicio { get; set; }

        [Description("Lançamento fim")]
        public DateTime? LancamentoFim { get; set; }

        [Description("Vencimento/Pagamento início")]
        public DateTime? VencimentoPagamentoInicio { get; set; }

        [Description("Vencimento/Pagamento fim")]
        public DateTime? VencimentoPagamentoFim { get; set; }

        [Description("Empresa")]
        public List<Empresa> Empresas { get; set; } = new List<Empresa>();

        public List<Lancamento> Lancamentos { get; set; }
    }
}
