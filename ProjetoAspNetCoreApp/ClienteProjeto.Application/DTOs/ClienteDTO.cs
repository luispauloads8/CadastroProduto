using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class ClienteDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe CPF do cliente")]
    [MinLength(3)]
    [MaxLength(11)]
    public string CPF { get; set; }

    [Required(ErrorMessage = "Informe a data de nascimento")]
    [DisplayFormat(DataFormatString = "mm/dd/yyyy")]
    public DateTime DataNascimento { get; set; }

    public string RG { get; set; }

    public EnumSexo Sexo { get; set; }

    public EnumEstadoCivil EstadoCivil { get; set; }

    [Required(ErrorMessage = "Informe nacionalidade")]
    [MinLength(3)]
    [MaxLength(100)]
    public string Nacionalidade { get; set; }

    [MaxLength(500)]
    public string Observacao { get; set; }

    public Pessoa Pessoa { get; set; }

    public ICollection<LancamentoDTO> Lancamentos { get; set; }

    public int PessoaId { get; set; }
}
