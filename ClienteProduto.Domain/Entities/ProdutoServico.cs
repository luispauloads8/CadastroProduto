using System.Reflection.Metadata;

namespace ClienteProjeto.Domain.Entities;

public class ProdutoServico : Entity
{
    public ProdutoServico(int id, string descricao, int categoriaId)
    {
        Id = id;
        Descricao = descricao;
        CategoriaId = categoriaId;
    }

    public string Descricao { get; set; }
    public Blob Imagem { get; set; }
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
    public ICollection<Lancamento> Lancamentos { get; set; }
}
