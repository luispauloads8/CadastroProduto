using Relatorio.Dto.Cliente;
using Relatorio.Dto.Empresa;
using Relatorio.Dto.Fornecedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Cidade
{
    [Serializable]
    public class ParametroEmissaoCidadeDto : ParametrosDeEmissaoBaseDR
    {
        public int Id { get; set; }

        public string Descricao { get; set; }
        //public EnumEstado Estado { get; set; }

        public ICollection<ParametroEmissaoClienteDto> Clientes { get; set; }
        public ICollection<ParametroEmissaoEmpresaDto> Empresas { get; set; }
        public ICollection<ParametroEmissaoFornecedoreDto> Fornecedores { get; set; }
    }
}
