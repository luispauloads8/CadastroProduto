namespace ClienteProjeto.Domain.Entities;

public class Cliente : Entity
{
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public string RG { get; set; }
    public EnumSexo Sexo { get; set; }
    public EnumEstadoCivil EstadoCivil { get; set; }
    public string Nacionalidade { get; set; }
    public string Observacao { get; set; }
    public Cidade Cidade { get; set; }
    public Pessoa Pessoa { get; set; }
    public ICollection<Lancamento> Lancamentos { get; set; }

    #region "EF"
    public int CidadeId { get; set; }
    public int PessoaId { get; set; }

    #endregion
}
