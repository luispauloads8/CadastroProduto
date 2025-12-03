using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ClienteProjeto.Domain.Entities;

public class Empresa : Entity
{

    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public string InscricaoEstadual { get; set; }
    public string Email { get; set; }
    public string CNPJ { get; set; }
    public string CNAE { get; set; }
    public string Telefone { get; set; }
    public Cidade Cidade { get; set; }
    public ICollection<Usuario> Usuarios { get; set; }
    public ICollection<Lancamento> Lancamentos { get; set; }
    public ICollection<ContaContabil> ContaContabils { get; set; }

    public ICollection<Pessoa> Pessoas { get; set; } = new List<Pessoa>();

    #region "EF"

    public int CidadeId { get; set; }

    #endregion

}
