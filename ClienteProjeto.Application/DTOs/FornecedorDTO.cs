using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class FornecedorDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe nome fornecedor")]
    [MinLength(3)]
    [MaxLength(300)]
    public string Descricao { get; set; }

    [Required(ErrorMessage = "Informe o CNPJ")]
    [MaxLength(14)]
    public string CNPJ { get; set; }

    [Required(ErrorMessage = "Informe telefone")]
    [MaxLength(15)]
    [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Formato de telefone inválido")]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "Informe o endereço")]
    [MinLength(3)]
    [MaxLength(300)]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "CEP é obrigatorio.")]
    [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O CEP deve estar no formato 12345-678.")]
    public string CEP { get; set; }

    [MaxLength(500)]
    public string Observacao { get; set; }

    public int CidadeId { get; set; }

    [Required(ErrorMessage = "Informe email")]
    [MinLength(3)]
    [MaxLength(150)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
}
