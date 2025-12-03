using ClienteProjeto.Domain.Entities;
using ClienteProjeto.Domain.Enum;
using ClienteProjeto.Infrastructure.EntitiesConfiguration;
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
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<Pessoa>()
            .HasOne(p => p.Empresa)
            .WithMany(e => e.Pessoas)
            .HasForeignKey(p => p.EmpresaId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProdutoServico>(entity => {
            entity.Property(e => e.Imagem).HasColumnType("LONGBLOB")
                  .IsRequired(false); 
        });

        modelBuilder.Entity<Pessoa>()
            .ToTable("Pessoas")
            .HasDiscriminator(p => p.TipoPessoa)
            .HasValue<PessoaFisica>(TipoPessoa.Fisica)
            .HasValue<PessoaJuridica>(TipoPessoa.Juridica);

        modelBuilder.Entity<Endereco>()
            .HasOne(e => e.Cidade)
            .WithMany()
            .HasForeignKey(e => e.CidadeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Cliente)
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.ClienteId)
            .HasConstraintName("FK_Pedido_Clientes_ClienteId")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ItensPedido>()
            .HasOne(i => i.Pedido)
            .WithMany(p => p.ItensPedido)
            .HasForeignKey(i => i.PedidoId)
            .HasConstraintName("FK_ItensPedido_Pedido_PedidoId")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItensPedido>()
            .HasOne(i => i.ProdutoServico)
            .WithMany(p => p.ItensPedido)
            .HasForeignKey(i => i.ProdutoServicoId)
            .HasConstraintName("FK_ItensPedido_ProdutoServicos_ProdutoServicoId")
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Empresa>()
            .HasMany(e => e.Pessoas)
            .WithOne(p => p.Empresa)
            .HasForeignKey(p => p.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<Pedido>()
            .Property(p => p.ValorTotal)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<ItensPedido>()
            .Property(p => p.ValorTotal)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<ItensPedido>()
            .Property(p => p.ValorItem)
            .HasColumnType("decimal(18,2)");

    }
}
