using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Observacao).HasMaxLength(500).IsRequired(); 

        builder.HasOne(x => x.Cidade)
            .WithMany(x => x.Fornecedores)
            .HasForeignKey(x => x.CidadeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
