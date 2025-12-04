using ClienteProjeto.Application.Conversao.ConversaoEntidade;
using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClienteProjeto.Application.DTOs
{
    [JsonConverter(typeof(PessoaDTOConverter))]
    public class PessoaDTO
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int EnderecoId { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Informe nome do cliente")]
        [MinLength(3)]
        [MaxLength(200)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe telefone")]
        [MaxLength(15)]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Formato de telefone inválido")]
        public string Telefone { get; set; }

        public string Nacionalidade { get; set; }


        [Required(ErrorMessage = "estado civil e obrigatorio")]
        public EnumEstadoCivil EstadoCivil { get; set; }

        [Required(ErrorMessage = "sexo e obrigatorio")]
        public EnumSexo Sexo { get; set; }

        public TipoPessoa TipoPessoa { get; set; }
        public Empresa Empresa { get; set; }
        public EnderecoDTO Endereco { get; set; }

    }
}
