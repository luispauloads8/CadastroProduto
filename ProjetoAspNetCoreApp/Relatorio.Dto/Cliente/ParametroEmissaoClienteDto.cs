using Relatorio.Dto.Cidade;
using Relatorio.Dto.Lancamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relatorio.Dto.Cliente
{
    [Serializable]
    public class ParametroEmissaoClienteDto : ParametrosDeEmissaoBaseDR
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string RG { get; set; }

        //        public EnumSexo Sexo { get; set; }

        public string CEP { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        //        public EnumEstadoCivil EstadoCivil { get; set; }

        public string Nacionalidade { get; set; }

        public string Observacao { get; set; }

        public int CidadeId { get; set; }

        public ParametroEmissaoCidadeDto Cidade { get; set; }
        public ICollection<ParametroEmissaoLancamentoDto> Lancamentos { get; set; }
    }
}
