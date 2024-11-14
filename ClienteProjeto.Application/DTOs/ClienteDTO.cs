using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class ClienteDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe nome do cliente")]
    [MinLength(3)]
    [MaxLength(200)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe CPF do cliente")]
    [MinLength(3)]
    [MaxLength(11)]
    public string CPF { get; set; }

    [Required(ErrorMessage = "Informe a data de nascimento")]
    [MaxLength (10)]
    [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
    public DateTime DataNascimento { get; set; }

    public string RG { get; set; }
    public string CEP { get; set; }

    [Required(ErrorMessage = "Informe o endereço")]
    [MinLength(3)]
    [MaxLength(300)]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "Informe telefone")]
    [MaxLength(15)]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "Informe email")]
    [MinLength (3)]
    [MaxLength(150)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [MinLength(3)]
    [MaxLength(100)]
    public Cidade Naturalidade { get; set; }

    [Required(ErrorMessage = "Informe nacionalidade")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Nacionalidade { get; set; }

    [MinLength(3)]
    [MaxLength(500)]
    public string Observacao { get; set; }
}
