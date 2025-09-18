using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.ProdutoServico
{
    public class ParametroEmissaoCategoriaDto : ParametrosDeEmissaoBaseDR
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public ICollection<ParametroEmissaoProdutoServicoDto> ProdutoServicos { get; set; }
    }
}
