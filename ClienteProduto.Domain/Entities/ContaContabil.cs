namespace ClienteProjeto.Domain.Entities;

public class ContaContabil : Entity
{
    public string Descricao { get; set; }
    public int GrupoContaId { get; set; }
    public GrupoConta GrupoConta { get; set; }
}
