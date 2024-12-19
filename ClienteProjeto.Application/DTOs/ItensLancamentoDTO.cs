using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class ItensLancamentoDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe a quantidade")]
    public int Quantidade { get; set; }

    [Required(ErrorMessage = "Informe o valor")]
    [DisplayFormat(DataFormatString = "{0:n2}")]
    public decimal ValorItem { get; set; }
}
