namespace ClienteProjeto.Domain.Entities;

public class Categoria : Entity
{
    public string Descricao { get; set; }
    public ICollection<ProdutoServico> ProdutoServicos { get; set; }
}
