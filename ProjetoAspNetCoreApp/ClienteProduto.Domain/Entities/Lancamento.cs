﻿namespace ClienteProjeto.Domain.Entities;

public class Lancamento : Entity
{
    public Lancamento()
    {
        
    }

    public Lancamento(int id, string observacao, DateTime dataLancamento, decimal valor)
    {
        Id = id;
        Observacao = observacao;
        DataLancamento = dataLancamento;
        Valor = valor;
    }

    public int EmpresaId { get; set; }
    public int ContaContabilId { get; set; }
    public int ProdutoServicoId { get; set; }
    public  int ClienteId { get; set; }
    public string Observacao { get; set; }
    public DateTime DataLancamento { get; set; }
    public decimal Valor { get; set; }
    public ContaContabil ContaContabil { get; set; }
    public Empresa Empresa { get; set; }
    public Cliente Cliente { get; set; }
    public ProdutoServico ProdutoServico { get; set; }
    public ICollection<ItensLancamento> ItensLancamentos { get; set; }
}
