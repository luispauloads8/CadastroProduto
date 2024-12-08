using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Application.Mappings;
using ClienteProjeto.Application.Services;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using ClienteProjeto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ClienteProjeto.CrossCutting.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 38))));

        services.AddScoped<IProdutoServicoRepository, ProdutoServicoRepository>();
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ICidadeRepository, CidadeRepository>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IContaContabilRepository, ContaContabilRepository>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        services.AddScoped<IGrupoContaRepository, GrupoContaRepository>();
        services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        services.AddScoped<IProdutoServicoService, ProdutoServicoService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<ICidadeService, CidadeService>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<IEmpresaService, EmpresaService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IGrupoContaService, GrupoContaService>();
        services.AddScoped<IContaContabilService, ContaContabilService>();


        services.AddAutoMapper(typeof(DTOMappingProfile));

        return services;
    }
}
