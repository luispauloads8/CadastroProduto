namespace ClienteProjeto.Domain.Entities;

public class Fornecedor : Entity
{
    public Fornecedor(int id, string descricao, string cnpj, string telefone, string endereco, string cep, string observacao, int cidadeId, string email)
    {
        Id = id;
        Descricao = descricao;
        CNPJ = cnpj;
        Telefone = telefone;
        Endereco = endereco;
        CEP = cep;
        Observacao = observacao;
        CidadeId = cidadeId;
        Email = email;
    }

    public string Descricao { get; set; }
    public string CNPJ { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public string CEP { get; set; }
    public string Observacao { get; set; }
    public int CidadeId { get; set; }
    public int EmpresaId { get; set; }
    public string Email { get; set; }
    public Cidade Cidade { get; set; }
    public Empresa Empresa { get; set; }
}
