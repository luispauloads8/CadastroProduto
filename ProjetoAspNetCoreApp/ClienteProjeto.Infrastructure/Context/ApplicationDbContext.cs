﻿using ClienteProjeto.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClienteProjeto.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    { }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<ContaContabil> ContaContabeis { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<GrupoConta> GrupoContas { get; set; }
    public DbSet<ItensLancamento> ItensLancamentos { get; set; }
    public DbSet<Lancamento> Lancamentos { get; set; }
    public DbSet<ProdutoServico> ProdutoServicos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);


        modelBuilder.Entity<ProdutoServico>(entity => {
            entity.Property(e => e.Imagem).HasColumnType("LONGBLOB")
                  .IsRequired(false); 
        });

        modelBuilder.Entity<Endereco>()
            .HasOne(e => e.Cidade)
            .WithMany()
            .HasForeignKey(e => e.CidadeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
