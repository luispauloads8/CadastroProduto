namespace ClienteProjeto.Domain.Entities;

public class Cidade : Entity
{
    public Cidade(int id, string descricao)
    {
        Id = id;
        Descricao = descricao;
    }

    public string Descricao { get; set; }
    public EnumEstado Estado { get; set;}
    public ICollection<Cliente> Clientes { get; set; }
    public ICollection<Empresa> Empresas { get; set; }
    public ICollection<Fornecedor> Fornecedores { get; set; }
}
