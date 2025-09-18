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
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(300);
            builder.Property(x => x.Email).HasMaxLength(300);
            builder.Property(x => x.CNPJ).HasMaxLength(20);
            builder.Property(x => x.Telefone).HasMaxLength(15);

            //builder.HasOne(x => x.Endereco)
            //    .WithMany(x => x.Pessoa)
            //    .HasForeignKey(x => x)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
