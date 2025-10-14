using ClienteProjeto.Application.ConversaoEntidade;
using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClienteProjeto.Application.DTOs;

public class ClienteDTO
{
    public int Id { get; set; }

    [MaxLength(500)]
    public string Observacao { get; set; }

    public PessoaDTO Pessoa { get; set; }

    public ICollection<LancamentoDTO> Lancamentos { get; set; }

    public int PessoaId { get; set; }
}
