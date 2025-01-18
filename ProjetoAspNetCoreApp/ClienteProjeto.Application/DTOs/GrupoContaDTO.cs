using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class GrupoContaDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe nome fornecedor")]
    [MinLength(3)]
    [MaxLength(300)]
    public string Descricao { get; set; }
    public ICollection<ContaContabilDTO> ContaContabeis { get; set; }
}
