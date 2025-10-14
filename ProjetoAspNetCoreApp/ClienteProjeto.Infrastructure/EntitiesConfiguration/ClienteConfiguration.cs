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

        builder
        .HasIndex(e => e.PessoaId)
        .IsUnique();

        builder.HasOne(x => x.Pessoa)
            .WithMany()
            .HasForeignKey(m => m.PessoaId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
