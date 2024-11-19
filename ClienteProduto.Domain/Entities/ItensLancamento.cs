namespace ClienteProjeto.Domain.Entities;

public class ItensLancamento : Entity
{
    public ItensLancamento(int id, int quantidade, decimal valorItem)
    {
        Id = id;
        Quantidade = quantidade;
        ValorItem = valorItem;
    }

    public int Quantidade { get; set; }
    public decimal ValorItem { get; set; }
    public int LancamentoId { get; set; }
    public Lancamento Lancamento { get; set; }
}
