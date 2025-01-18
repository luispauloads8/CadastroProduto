using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class CategoriaDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe a descrição da categoria")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Descricao { get; set; }

    public ICollection<ProdutoServicoDTO> ProdutoServicos { get; set; }
}
