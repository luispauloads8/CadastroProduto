using Relatorio.Dto.ContaContabil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.GrupoConta
{
    [Serializable]
    public class ParametroEmissaoGrupoContaDto : ParametrosDeEmissaoBaseDR
    {
        public int Id { get; set; }

        public string Descricao { get; set; }
        public ICollection<ParametroEmissaoContaContabilDto> ContaContabeis { get; set; }
    }
}
