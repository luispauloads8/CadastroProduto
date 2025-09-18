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
    public class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.Property(x => x.Logradouro).HasMaxLength(250);
            builder.Property(x => x.Bairro).HasMaxLength(100);
            builder.Property(x => x.CEP).HasMaxLength(15);
            builder.Property(x => x.CaixaPostal).HasMaxLength(20);

        }
    }
}
