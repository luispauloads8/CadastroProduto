using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(300);
            builder.Property(x => x.Email).HasMaxLength(300);
            builder.Property(x => x.Telefone).HasMaxLength(15);
            builder.Property(x => x.Sexo).HasMaxLength(1).IsRequired();

            // TPH - uma única tabela Pessoas
            builder.HasDiscriminator<TipoPessoa>("TipoPessoa")
            .HasValue<PessoaFisica>(TipoPessoa.Fisica)
            .HasValue<PessoaJuridica>(TipoPessoa.Juridica);


        }
    }
}
