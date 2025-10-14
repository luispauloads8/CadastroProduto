namespace ClienteProjeto.Domain.Entities;

public class Cliente : Entity
{
    public string Observacao { get; set; }

    public Pessoa Pessoa { get; set; }
    public ICollection<Lancamento> Lancamentos { get; set; }
    public ICollection<Pedido> Pedidos { get; set; }

    #region "EF"
    public int PessoaId { get; set; }

    #endregion
}
