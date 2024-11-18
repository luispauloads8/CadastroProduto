using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class ItensLancamentoConfiguration : IEntityTypeConfiguration<ItensLancamento>
{
    public void Configure(EntityTypeBuilder<ItensLancamento> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Quantidade)
            .IsRequired()
            .HasDefaultValue(0)
            .HasColumnType("int");

        builder.Property(x => x.ValorItem)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.HasOne(x => x.Lancamento)
            .WithMany(x => x.ItensLancamentos)
            .HasForeignKey(x => x.LancamentoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
