namespace ClienteProjeto.Domain.Entities;

public class ContaContabil : Entity
{

    public string Descricao { get; set; }
    public GrupoConta GrupoConta { get; set; }
    public Empresa Empresa { get; set; }
    public ICollection<Lancamento> Lancamentos { get; set; }

    #region "EF"

    public int EmpresaId { get; set; }

    public int GrupoContaId { get; set; }

    #endregion
}
