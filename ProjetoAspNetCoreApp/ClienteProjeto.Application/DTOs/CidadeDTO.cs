﻿using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class CidadeDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe descrição da cidade")]
    [MinLength(3)]
    [MaxLength(50)]
    public string Descricao { get; set; }
    public EnumEstado Estado { get; set; }

    public ICollection<ClienteDTO> Clientes { get; set; }
    public ICollection<EmpresaDTO> Empresas { get; set; }
    public ICollection<FornecedorDTO> Fornecedores { get; set; }
}
