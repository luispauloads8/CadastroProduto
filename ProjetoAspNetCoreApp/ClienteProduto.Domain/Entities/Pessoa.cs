using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain.Entities
{
    public class Pessoa : Entity
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CNPJ { get; set; }
        public Endereco Endereco { get; set; }


        #region "EF"
        public int EnderecoId { get; set; }

        #endregion
    }
}
