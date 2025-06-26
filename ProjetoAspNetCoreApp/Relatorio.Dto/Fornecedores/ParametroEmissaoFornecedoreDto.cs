using Relatorio.Dto.Cidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Fornecedores
{
    [Serializable]
    public class ParametroEmissaoFornecedoreDto : ParametrosDeEmissaoBaseDR
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string CNPJ { get; set; }

        public string Telefone { get; set; }

        public string Endereco { get; set; }

        public string CEP { get; set; }

        public string Observacao { get; set; }

        public int CidadeId { get; set; }

        public string Email { get; set; }

        public ParametroEmissaoCidadeDto Cidade { get; set; }
    }
}
