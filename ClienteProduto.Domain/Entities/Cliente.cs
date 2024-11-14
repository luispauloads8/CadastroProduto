namespace ClienteProjeto.Domain.Entities;

public class Cliente : Entity
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public string RG { get; set; }
    public EnumSexo Sexo { get; set; }
    public string CEP { get; set; }
    public string Endereco { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public EnumEstadoCivil EstadoCivil { get; set; }
    public Cidade Naturalidade { get; set; }
    public string Nacionalidade { get; set; }
    public string Observacao { get; set; }
    public int CidadeId { get; set; }
}
