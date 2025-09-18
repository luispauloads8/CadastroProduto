﻿using System.Text.Json.Serialization;

namespace ClienteProjeto.Domain.Entities;

public class ProdutoServico : Entity
{
    public ProdutoServico(int id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }

    public string Descricao { get; set; }
    public double Preco { get; set; }
    public byte[] Imagem { get; set; }
    public int CategoriaId { get; set; }

    public Categoria Categoria { get; set; }

    [JsonIgnore]
    public ICollection<Lancamento> Lancamentos { get; set; }
    public ICollection<Pedido> Pedidos { get; set; }
}
