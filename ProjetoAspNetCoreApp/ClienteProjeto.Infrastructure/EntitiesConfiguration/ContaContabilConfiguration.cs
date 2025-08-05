using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class ContaContabilConfiguration : IEntityTypeConfiguration<ContaContabil>
{
    public void Configure(EntityTypeBuilder<ContaContabil> builder)
    {
        builder.HasKey(x  => x.Id);
        builder.Property(x => x.Descricao).HasMaxLength(300).IsRequired();

        builder.HasOne(x => x.GrupoConta)
            .WithMany(x => x.ContaContabeis)
            .HasForeignKey(x => x.GrupoContaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Empresa)
           .WithMany(e => e.ContaContabils)
           .HasForeignKey(x => x.EmpresaId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
