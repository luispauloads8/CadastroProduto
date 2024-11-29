using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class ProdutoServicoConfiguration : IEntityTypeConfiguration<ProdutoServico>
{
    public void Configure(EntityTypeBuilder<ProdutoServico> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Descricao).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Imagem).HasColumnType("varbinary(max)").IsRequired();

        builder.HasOne(x => x.Categoria)
            .WithMany(x => x.ProdutoServicos)
            .HasForeignKey(x => x.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
