namespace ClienteProjeto.Domain.Entities;

public class Usuario : Entity
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int EmpresaId { get; set; }
    public Empresa Empresa { get; set; }
}
