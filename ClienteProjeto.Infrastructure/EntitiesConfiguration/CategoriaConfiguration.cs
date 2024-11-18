using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Descricao).HasMaxLength(100).IsRequired();

        builder.HasData(
            new Categoria(1, "Eletrônicos")
        );
    }
}
