using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Observacao).HasMaxLength(500);
        builder.Property(x => x.CPF).HasMaxLength(11).IsRequired();
        builder.Property(x => x.DataNascimento).HasMaxLength(10).IsRequired();
        builder.Property(x => x.Nacionalidade).HasMaxLength(100).IsRequired();
        builder.Property(x => x.RG).HasMaxLength(10).IsRequired();
        builder.Property(x => x.Sexo).HasMaxLength(1).IsRequired();
        builder.Property(x => x.EstadoCivil).HasMaxLength(1).IsRequired();

        builder
        .HasIndex(e => e.PessoaId)
        .IsUnique();

        builder.HasOne(x => x.Pessoa)
            .WithMany()
            .HasForeignKey(m => m.PessoaId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
