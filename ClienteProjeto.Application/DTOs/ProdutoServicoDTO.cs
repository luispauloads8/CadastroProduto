using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class ProdutoServicoDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O descrição e obrigatoria")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Descricao { get; set; }
    public byte[] Imagem { get; set; }
    public int CategoriaId { get; set; }
}
