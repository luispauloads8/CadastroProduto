using System.Text.Json.Serialization;

namespace ClienteProjeto.Domain.Entities;

public class Categoria : Entity
{
    public Categoria(int id, string descricao)
    {
        Id = id;
        Descricao = descricao;
        
    }

    public string Descricao { get; set; }

    [JsonIgnore]
    public ICollection<ProdutoServico> ProdutoServicos { get; set; }
}
