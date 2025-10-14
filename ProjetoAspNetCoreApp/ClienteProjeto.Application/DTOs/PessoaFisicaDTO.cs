using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs
{
    public class PessoaFisicaDTO : PessoaDTO
    {
        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }
    }
}
