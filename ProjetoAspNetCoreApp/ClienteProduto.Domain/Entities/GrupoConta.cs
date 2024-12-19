namespace ClienteProjeto.Domain.Entities
{
    public class GrupoConta : Entity
    {
        public GrupoConta(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }

        public string Descricao { get; set; }
        public ICollection<ContaContabil> ContaContabeis { get; set; }
    }
}
