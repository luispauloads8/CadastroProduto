using Relatorio.Dto.Cliente;
using Relatorio.Dto.ContaContabil;
using Relatorio.Dto.Empresa;
using Relatorio.Dto.Modelos;
using Relatorio.Dto.ProdutoServico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Relatorio.Dto.Lancamento
{
    [Serializable]
    public class ParametroEmissaoLancamentoDto : ParametrosDeEmissaoBaseDR
    {
        public int Id { get; set; }

        public string Observacao { get; set; }

        public DateTime DataLancamento { get; set; }

        public decimal Valor { get; set; }

        public int EmpresaId { get; set; }
        public int ContaContabilId { get; set; }
        public int ProdutoServicoId { get; set; }
        public int ClienteId { get; set; }

        public DtoLancamento Lancamento { get; set; }

        [JsonPropertyName("ContaContabilDTO")]
        public DtoContaContabil ContaContabil { get; set; }

        [JsonPropertyName("EmpresaDTO")]
        public DtoEmpresa Empresa { get; set; }

        [JsonPropertyName("ClienteDTO")]
        public DtoCliente Cliente { get; set; }

        [JsonPropertyName("ProdutoServicoDTO")]
        public DtoProdutoServico ProdutoServico { get; set; }

        [JsonPropertyName("ItensLancamentoDTO")]
        public ICollection<DtoItensLancamento> ItensLancamentos { get; set; }
    }
}
