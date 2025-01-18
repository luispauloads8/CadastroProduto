using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class CidadeDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe descrição da cidade")]
    [MinLength(3)]
    [MaxLength(50)]
    public string Descricao { get; set; }

    public ICollection<ClienteDTO> ClientesEndereco { get; set; }
    public ICollection<EmpresaDTO> Empresas { get; set; }
    public ICollection<FornecedorDTO> Fornecedores { get; set; }
}
