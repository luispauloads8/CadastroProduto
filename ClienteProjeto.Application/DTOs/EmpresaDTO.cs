using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class EmpresaDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe razão social")]
    [MinLength(3)]
    [MaxLength(300)]
    public string RazaoSocial { get; set; }

    [Required(ErrorMessage = "Informe Nome Fantasia"]
    [MinLength(3)]
    [MaxLength(300)]
    public string NomeFantasia { get; set; }

    [Required(ErrorMessage = "")]
    public string CNPJ { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
}
