using System.ComponentModel.DataAnnotations;
using System.Runtime;

namespace ClienteProjeto.Application.DTOs;

public class CategoriaDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe a descrição da categoria")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Descricao { get; set; }

}
