namespace ClienteProjeto.Domain.Entities;

public class Empresa : Entity
{
    public string RazaoSocial { get; set; }
    public string NomeFantasia { get; set; }
    public string CNPJ { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
}
