using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration;

public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.NomeFantasia).HasMaxLength(300).IsRequired();
        builder.Property(x => x.RazaoSocial).HasMaxLength(300).IsRequired();
        builder.Property(x => x.CNPJ).HasMaxLength(14).IsRequired().HasColumnType("char(14");
        builder.Property(x => x.Telefone).HasMaxLength(15).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(150).IsRequired();

        builder.HasOne(x => x.Cidade)
            .WithMany(x => x.Empresas)
            .HasForeignKey(x => x.CidadeEmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
