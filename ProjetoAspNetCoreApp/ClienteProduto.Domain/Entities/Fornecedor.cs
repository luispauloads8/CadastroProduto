namespace ClienteProjeto.Domain.Entities;

public class Fornecedor : Entity
{

    public string Observacao { get; set; }
    
    public Cidade Cidade { get; set; }
    public Pessoa Pessoa { get; set; }

    #region "EF"
    public int CidadeId { get; set; }
    public int PessoaId { get; set; }

    #endregion
}
