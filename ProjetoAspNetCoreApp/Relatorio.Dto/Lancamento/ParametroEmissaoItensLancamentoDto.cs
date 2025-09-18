using Relatorio.Dto.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Relatorio.Dto.Lancamento
{
    [Serializable]
    public class ParametroEmissaoItensLancamentoDto : ParametrosDeEmissaoBaseDR
    {
        public int Id { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorItem { get; set; }
        public int LancamentoId { get; set; }

        [JsonPropertyName("ItensLancamentoDTO")]
        public DtoLancamento Lancamento { get; set; }
    }
}
