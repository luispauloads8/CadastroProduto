using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Domain.Entities
{
    public class Endereco : Entity
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string CaixaPostal { get; set; }
        public string CEP { get; set; }
        public Cidade Cidade { get; set; }

        #region "EF"
        public int CidadeId { get; set; }

        #endregion
    }
}
