using Relatorio.Dto.GrupoConta;
using Relatorio.Dto.Lancamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.ContaContabil
{
    [Serializable]
    public class ParametroEmissaoContaContabilDto : ParametrosDeEmissaoBaseDR
    {
        public int Id { get; set; }

        public string Descricao { get; set; }
        public int GrupoContaId { get; set; }

        public ParametroEmissaoGrupoContaDto GrupoConta { get; set; }
        public ICollection<ParametroEmissaoLancamentoDto> Lancamentos { get; set; }
    }
}
