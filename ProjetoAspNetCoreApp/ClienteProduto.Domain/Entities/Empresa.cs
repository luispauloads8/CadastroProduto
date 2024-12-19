namespace ClienteProjeto.Domain.Entities;

public class Empresa : Entity
{

    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public string CNPJ { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public int CidadeEmpresaId { get; set; }
    public Cidade Cidade { get; set; }
    public ICollection<Usuario> Usuarios { get; set; }
    public ICollection<Lancamento> Lancamentos { get; set; }

}
