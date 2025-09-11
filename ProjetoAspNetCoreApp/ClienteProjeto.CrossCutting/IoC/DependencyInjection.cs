using ClienteProjeto.Application.Interfaces;
using ClienteProjeto.Application.Interfaces.Emissao;
using ClienteProjeto.Application.Mappings;
using ClienteProjeto.Application.Services;
using ClienteProjeto.Application.Services.Emissao;
using ClienteProjeto.Domain.Interfaces;
using ClienteProjeto.Infrastructure.Context;
using ClienteProjeto.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        services.AddScoped<IPessoaRepository, PessoaRepository>();
        services.AddScoped<IEnderecoRepository, EnderecoRepository>();
        services.AddScoped<IContaContabilRepository, ContaContabilRepository>();
        services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        services.AddScoped<IGrupoContaRepository, GrupoContaRepository>();
        services.AddScoped<ILancamentoRepository, LancamentoRepository>();
        services.AddScoped<IItensLancamentosRepository, ItensLancamentoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        services.AddScoped<IProdutoServicoService, ProdutoServicoService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<ICidadeService, CidadeService>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<IEmpresaService, EmpresaService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IPessoaService, PessoaService>();
        services.AddScoped<IEnderecoService, EnderecoService>();
        services.AddScoped<IGrupoContaService, GrupoContaService>();
        services.AddScoped<IContaContabilService, ContaContabilService>();
        services.AddScoped<ILancamentoService, LancamentoService>();

        services.AddScoped<IEmissaoLancamentoService, EmissaoLancamentoService>();
        services.AddScoped<IEmissaoCategoriaService, EmissaoCategoriaService>();

        services.AddScoped<ITokenService, TokenService>();
        

        services.AddAutoMapper(typeof(DTOMappingProfile));
        services.AddAutoMapper(typeof(DTOMappingProfilesViewModel));
        services.AddAutoMapper(typeof(DTOMappingProfileFiltro));
        services.AddAutoMapper(typeof(DTOMappingProfileViewModelEntidade));

        return services;
    }
}
