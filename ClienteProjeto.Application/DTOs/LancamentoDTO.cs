using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class LancamentoDTO
{
    public int Id { get; set; }

    [MaxLength(500)]
    public string Observacao { get; set; }
    public int EmpresaId { get; set; }
    public int ContaContabilId { get; set; }
    public int ProdutoServicoId { get; set; }
}
