using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs
{
    public class PessoaJuridicaDTO : PessoaDTO
    {
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string CNAE { get; set; }
        [RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve conter apenas 14 dígitos numéricos.")]
        public string CNPJ { get; set; }
    }
}
