using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class LancamentoDTO
{
    public int Id { get; set; }

    [MaxLength(500)]
    public string Observacao { get; set; }

    [Required(ErrorMessage = "Informar a data do lançamento")]
    [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
    public DateTime DataLancamento { get; set; }

    [Required(ErrorMessage = "Informe o valor")]
    [DisplayFormat(DataFormatString = "{0:n2}")]
    public decimal Valor { get; set; }

    public int EmpresaId { get; set; }
    public int ContaContabilId { get; set; }
    public int ProdutoServicoId { get; set; }
    public int ClienteId { get; set; }

    public ContaContabilDTO ContaContabil { get; set; }
    public EmpresaDTO Empresa { get; set; }
    public ClienteDTO Cliente { get; set; }
    public ProdutoServicoDTO ProdutoServico { get; set; }
    public ICollection<ItensLancamentoDTO> ItensLancamentos { get; set; }
}
