using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Nome).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Endereco).HasMaxLength(300).IsRequired();
        builder.Property(x => x.CEP).HasMaxLength(8).IsRequired();
        builder.Property(x => x.Observacao).HasMaxLength(500);
        builder.Property(x => x.CPF).HasMaxLength(11).IsRequired();
        builder.Property(x => x.DataNascimento).HasMaxLength(10).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Nacionalidade).HasMaxLength(100).IsRequired();
        builder.Property(x => x.RG).HasMaxLength(10).IsRequired();
        builder.Property(x => x.Telefone).HasMaxLength(15).IsRequired();
        builder.Property(x => x.Sexo).HasMaxLength(1).IsRequired();
        builder.Property(x => x.EstadoCivil).HasMaxLength(1).IsRequired();        
            
        builder.HasOne(c => c.Cidade)
            .WithMany(c => c.ClientesEndereco)
            .HasForeignKey(c => c.CidadeEnderecoId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
