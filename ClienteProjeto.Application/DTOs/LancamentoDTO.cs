using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class LancamentoDTO
{
    public int Id { get; set; }

    [MaxLength(500)]
    public string Observacao { get; set; }

    [Required(ErrorMessage = "Informar a data do lançamento")]
    [MaxLength(10)]
    [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
    public DateTime DataLancamento { get; set; }

    [Required(ErrorMessage = "Informe o valor")]
    [DisplayFormat(DataFormatString = "{0:n2}")]
    public decimal Valor { get; set; }

    public int EmpresaId { get; set; }
    public int ContaContabilId { get; set; }
    public int ProdutoServicoId { get; set; }
}
