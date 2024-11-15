using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class EmpresaDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe razão social")]
    [MinLength(3)]
    [MaxLength(300)]
    public string RazaoSocial { get; set; }

    [Required(ErrorMessage = "Informe Nome Fantasia")]
    [MinLength(3)]
    [MaxLength(300)]
    public string NomeFantasia { get; set; }

    [Required(ErrorMessage = "Informe o CNPJ")]
    [MaxLength(14)]
    public string CNPJ { get; set; }

    [Required(ErrorMessage = "Informe email")]
    [MinLength(3)]
    [MaxLength(150)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Informe telefone")]
    [MaxLength(15)]
    public string Telefone { get; set; }
}
