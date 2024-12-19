using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class FornecedorConfiguration : IEntityTypeConfiguration<Fornecedor>
{
    public void Configure(EntityTypeBuilder<Fornecedor> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Descricao).HasMaxLength(300).IsRequired();
        builder.Property(x => x.Endereco).HasMaxLength(300).IsRequired();
        builder.Property(x => x.Observacao).HasMaxLength(500).IsRequired();
        builder.Property(x => x.CNPJ).HasMaxLength(14).IsRequired();
        builder.Property(x => x.CEP).HasMaxLength(10).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(150).IsRequired();
        builder.Property(x => x.Telefone).HasMaxLength(15).IsRequired();

        builder.HasOne(x => x.Cidade)
            .WithMany(x => x.Fornecedores)
            .HasForeignKey(x => x.CidadeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
