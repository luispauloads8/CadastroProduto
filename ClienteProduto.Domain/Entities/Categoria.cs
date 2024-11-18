namespace ClienteProjeto.Domain.Entities;

public class Categoria : Entity
{
    public Categoria(int id, string descricao)
    {
        Id = id;
        Descricao = descricao;
        
    }

    public string Descricao { get; set; }
    public ICollection<ProdutoServico> ProdutoServicos { get; set; }
}
