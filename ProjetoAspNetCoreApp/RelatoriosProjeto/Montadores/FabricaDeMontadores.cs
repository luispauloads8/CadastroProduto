using ClienteProjeto.API.Montadores.Service;
using Relatorio.Dto;

namespace RelatoriosProjeto.Montadores
{
    public static class FabricaDeMontadores
    {
        public static IMontador Crie(string nomeDaClasseCompleta, ParametrosDeEmissaoBaseDR parametros)
        {
            // Busca por nome simples da classe, ignorando abstratas
            var tipoMontador = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t =>
                    t.Name == nomeDaClasseCompleta &&
                    typeof(IMontador).IsAssignableFrom(t) &&
                    !t.IsAbstract // <- ESSENCIAL para evitar cair em MontadorAbstrato
                );

            if (tipoMontador == null)
                throw new ArgumentException($"Tipo '{nomeDaClasseCompleta}' não encontrado.");

            // Verifica se o tipo implementa IMontador
            if (!typeof(IMontador).IsAssignableFrom(tipoMontador))
                throw new InvalidOperationException($"Tipo '{nomeDaClasseCompleta}' não implementa IMontador.");

            // Cria instância via Activator
            object? instancia = Activator.CreateInstance(tipoMontador, parametros);

            return instancia as IMontador
                ?? throw new InvalidCastException($"Não foi possível converter a instância para IMontador.");
        }
        
    }
}
