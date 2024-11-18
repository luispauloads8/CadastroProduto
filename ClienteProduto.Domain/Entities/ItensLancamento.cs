namespace ClienteProjeto.Domain.Entities;

public class ItensLancamento : Entity
{
    public ItensLancamento(int id, int quantidade, decimal valorItem, int lancamentoId)
    {
        Id = id;
        Quantidade = quantidade;
        ValorItem = valorItem;
        LancamentoId = lancamentoId;
    }

    public int Quantidade { get; set; }
    public decimal ValorItem { get; set; }
    public int LancamentoId { get; set; }
    public Lancamento Lancamento { get; set; }
}
