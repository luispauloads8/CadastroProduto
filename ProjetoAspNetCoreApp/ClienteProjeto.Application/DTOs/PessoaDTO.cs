using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs
{
    public class PessoaDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Informe nome do cliente")]
        [MinLength(3)]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe telefone")]
        [MaxLength(15)]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Formato de telefone inválido")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "O CNPJ deve conter apenas 14 dígitos numéricos.")]
        public string CNPJ { get; set; }
        public EnderecoDTO Endereco { get; set; }

        public int EnderecoId { get; set; }
    }
}
