using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ClienteProjeto.Infrastructure.Context;

public class AppLicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Ajuste o caminho para o diretório onde o appsettings.json está localizado
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "../ClienteProjeto.API");

        // Carrega as configurações do appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

        // Configura o DbContextOptionsBuilder com o MySQL
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 38)));

        return new ApplicationDbContext(builder.Options);
    }
}
