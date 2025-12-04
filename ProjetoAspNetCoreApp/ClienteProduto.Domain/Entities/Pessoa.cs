using ClienteProjeto.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain.Entities
{
    public abstract class Pessoa : Entity
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public EnumEstadoCivil EstadoCivil { get; set; }
        public string Nacionalidade { get; set; }
        public Endereco Endereco { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public EnumSexo Sexo { get; set; }
        public Empresa Empresa { get; set; }


        #region "EF"
        public int EnderecoId { get; set; }
        public int EmpresaId { get; set; }

        #endregion
    }
}
