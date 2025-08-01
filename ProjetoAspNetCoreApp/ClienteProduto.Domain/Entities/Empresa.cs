using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace ClienteProjeto.Domain.Entities;

public class Empresa : Entity
{

    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public Cidade Cidade { get; set; }
    public Pessoa Pessoa { get; set; }
    public ICollection<Usuario> Usuarios { get; set; }
    public ICollection<Lancamento> Lancamentos { get; set; }
    public ICollection<ContaContabil> ContaContabils { get; set; }

    #region "EF"

    public int CidadeId { get; set; }
    public int PessoaId { get; set; }

    #endregion

}
