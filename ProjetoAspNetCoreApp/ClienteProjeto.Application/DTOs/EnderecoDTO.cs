using ClienteProjeto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs
{
    public class EnderecoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe endereço")]
        [MinLength(3)]
        [MaxLength(300)]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Informe bairro")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe nome fornecedor")]
        [MinLength(3)]
        [MaxLength(300)]
        public string CaixaPostal { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "O CEP deve estar no formato 99999-999 ou 99999999")]
        public string CEP { get; set; }

        public CidadeDTO Cidade { get; set; }

        #region "EF"
        public int CidadeId { get; set; }

        #endregion
    }
}
