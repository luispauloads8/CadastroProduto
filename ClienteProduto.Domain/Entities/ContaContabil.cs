namespace ClienteProjeto.Domain.Entities;

public class ContaContabil : Entity
{
    public ContaContabil(int id, string descricao, int grupoContaId)
    {
        Id = id;
        Descricao = descricao;
        GrupoContaId = grupoContaId;
    }

    public string Descricao { get; set; }
    public int GrupoContaId { get; set; }
    public GrupoConta GrupoConta { get; set; }
    public ICollection<Lancamento> Lancamentos { get; set; }
}
