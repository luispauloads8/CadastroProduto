using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class LancamentoConfiguration : IEntityTypeConfiguration<Lancamento>
{
    public void Configure(EntityTypeBuilder<Lancamento> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Observacao).HasMaxLength(500);
        builder.Property(x => x.ItensLancamentos).HasMaxLength(10); 
        builder.Property(x => x.Valor).IsRequired().HasPrecision(18, 2).HasDefaultValue(0);

        builder.HasOne(x => x.Empresa)
            .WithMany(x => x.Lancamentos)
            .HasForeignKey(x => x.EmpresaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Cliente)
            .WithMany(x => x.Lancamentos)
            .HasForeignKey(x => x.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ProdutoServico)
            .WithMany(x => x.Lancamentos)
            .HasForeignKey(x => x.ProdutoServicoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.ContaContabil)
            .WithMany(x => x.Lancamentos)
            .HasForeignKey(x => x.ContaContabilId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
