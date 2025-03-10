using ClienteProjeto.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ClienteProjeto.Application.DTOs;

public class UsuarioDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatorio")]
    [MinLength(3)]
    [MaxLength(200)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email é obrigatorio")]
    [MinLength(3)]
    [MaxLength(150)]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }


    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 100 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$!%*?&])[A-Za-z\d@#$!%*?&]{8,}$",
        ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
    public string Password { get; set; }
    public int EmpresaId { get; set; }
    public EmpresaDTO Empresa { get; set; }
}
