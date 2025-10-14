using ClienteProjeto.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteProjeto.Infrastructure.EntitiesConfiguration
{
    public class PessoaFisicaConfiguration : IEntityTypeConfiguration<PessoaFisica>
    {
        public void Configure(EntityTypeBuilder<PessoaFisica> builder)
        {
            builder.Property(x => x.CPF)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(x => x.DataNascimento)
                .IsRequired();
        }
    }
}
