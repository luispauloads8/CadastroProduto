using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class GrupoContaConfiguration : IEntityTypeConfiguration<GrupoConta>
{
    public void Configure(EntityTypeBuilder<GrupoConta> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Descricao).HasMaxLength(100).IsRequired();
    }
}
