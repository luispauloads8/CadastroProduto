using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class ContaContabilDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe descrição do da conta contabil")]
    [MinLength(3)]
    [MaxLength(300)]
    public string Descricao { get; set; }
    public int GrupoContaId { get; set; }
    public int EmpresaId { get; set; }
    public Empresa Empresa { get; set; }

    public GrupoContaDTO GrupoConta { get; set; }
    public ICollection<LancamentoDTO> Lancamentos { get; set; }
}
