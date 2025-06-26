using Relatorio.Dto.Cidade;
using Relatorio.Dto.Lancamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Empresa
{
    [Serializable]
    public class ParametroEmissaoEmpresaDto : ParametrosDeEmissaoBaseDR
    {
        public int Id { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public string CNPJ { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public int CidadeId { get; set; }

        public ParametroEmissaoCidadeDto Cidade { get; set; }
        //public ICollection<UsuarioDTO> Usuarios { get; set; }
        public ICollection<ParametroEmissaoLancamentoDto> Lancamentos { get; set; }
    }
}
